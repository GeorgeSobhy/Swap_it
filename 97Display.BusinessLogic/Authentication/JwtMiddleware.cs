using SwapIt.BusinessLogic.Authentication.Models;
using SwapIt.Data.Entities;
using RquestContext.Configuration;
using SwapIt.Mapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;

namespace SwapIt.BusinessLogic.Authentication
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService, IService<User, UserModel> userService )
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenService.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation

                var userIncludes = new System.Collections.Generic.List<System.Linq.Expressions.Expression<Func<User, object>>>();
                userIncludes.Add(x => x.Role);
                var user = userService.QueryModel(x => x.Id == userId.Value, userIncludes).FirstOrDefault();

                if (user != null)
                { 
                    //Get User Priviliges 
                        
                    context.Items["User"] = new AuthenticateResponse(user, token);

                    context.Items["UserId"] = userId;
                }
            }

            await _next(context);
        }
    }
}
