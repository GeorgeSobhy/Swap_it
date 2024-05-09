using SwapIt.BusinessLogic.Authentication.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace SwapIt.BusinessLogic.Authentication.Attributes.WebApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        readonly string[] _claim;
        readonly string[] _privileges;

        public AuthorizeAttribute(string[]? privileges = null, params string[] claim)
        {
            _claim = claim;
            _privileges = privileges;
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
                context.Result = new RedirectResult("~/Account/Login");

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
                    context.Result = new RedirectResult("~/Account/Login");
                }
                 

            }

            return;

        }
    }



}
