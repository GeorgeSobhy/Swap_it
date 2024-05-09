using SwapIt.BusinessLogic.Authentication.Models;
using SwapIt.Data.Entities;
using RquestContext.Configuration;
using SwapIt.Mapper.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Pattern;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwapIt.BusinessLogic.Authentication
{
    public interface ITokenService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        public long? ValidateToken(string token);
    }

    public class TokenService : ITokenService

    {

        private readonly ConfigurationValuesModel _configValuesSettings;

        private readonly IService<User, UserModel> _userService; 
        public TokenService(IOptions<ConfigurationValuesModel> configValuesSettings,  IService<User, UserModel> userService)
        {
            _configValuesSettings = configValuesSettings.Value;
            _userService = userService; 
        }
        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {

            var personIncludes = new System.Collections.Generic.List<System.Linq.Expressions.Expression<Func<User, object>>>();
            personIncludes.Add(x => x.Role);
            //var person = _userService.QueryModel(x => x.Email == model.Email && x.Password == EncryptionHelper.Encrypt(model.Password), personIncludes).FirstOrDefault();

            var user = _userService.QueryModel(x => x.Email == model.Email && x.Password ==  model.Password , personIncludes).FirstOrDefault();

            // return null if user not found
            if (user == null)
                return null;

           
           
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            // Returns User details and Jwt token in HttpResponse which will be user to authenticate the user.
            return new AuthenticateResponse(user, token);
        }

        //Generate Jwt Token
        private string generateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configValuesSettings.TokenKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Here you  can fill claim information from database for the usere as well
            var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Name.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Role",user.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configValuesSettings.TokenIssuer, _configValuesSettings.TokenIssuer,
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public long? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.UTF8.GetBytes(_configValuesSettings.TokenKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

    }

}
