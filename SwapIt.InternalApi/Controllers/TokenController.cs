using SwapIt.BusinessLogic.Authentication.Models;
using SwapIt.BusinessLogic.Authentication; 
using Microsoft.AspNetCore.Mvc;

namespace SwapIt.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
             _tokenService = tokenService;
        }

        
        [HttpPost()]
        public IActionResult Post([FromForm] AuthenticateRequest model)
        {
            var response = _tokenService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(response); 
        }
    }
}
