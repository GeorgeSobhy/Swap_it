
using SwapIt.Mapper.Models;
namespace SwapIt.BusinessLogic.Authentication.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public AuthenticateResponse(UserModel person, string token)
        {
            Id = person.Id;
            Name = person.Name;
            Email = person.Email;
            Token = token;
            Role = person.RoleName ?? string.Empty;

        }
    }
}
