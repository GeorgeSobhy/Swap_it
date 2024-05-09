using SwapIt.BusinessLogic.Authentication.Models;
using SwapIt.Mapper.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwapIt.BusinessLogic.Authentication.Attributes.WebAPI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        readonly string[] _claim; 

        public AuthorizeAttribute(  params string[] claim )
        {
            _claim = claim; 
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;


            // authorization         
            var user = (AuthenticateResponse)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
            else
            {

                 
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    // Check roles   
                    if (user.Role.Equals(item, StringComparison.OrdinalIgnoreCase))
                    {
                        flagClaim = true;
                    }
                }

                if (!flagClaim)
                {
                    context.Result = new JsonResult(new { message = "No Permission" }) { StatusCode = StatusCodes.Status403Forbidden };
                }

                 

            }

            return;

        }
    }
}
