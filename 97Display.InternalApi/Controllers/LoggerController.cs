using SwapIt.BusinessLogic.Authentication.Attributes.WebAPI;
using SwapIt.BusinessLogic.Services;
using SwapIt.Mapper.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SwapIt.Api.Controllers
{
    [Authorize(claim: new[] { "Provider", "Admin", "Customer" })]
    [ApiController]
    [Route("[controller]")]
    public class LoggerController : ControllerBase
    {

        private const String CLASS_NAME = "LoggerController";

        private readonly ILogService _logService;
        public LoggerController(ILogService logService)
        {

            _logService = logService;
        }

        [HttpPost("AddErrorLog")]
        public async Task<bool> AddErrorLog(ErrorLogModel model)
        {
            const string METHOD_NAME = "AddErrorLog";
            try
            {
                _logService.AddErrorLog(model);
                return true;
            }
            catch (Exception ex)
            {
                var jsonRes = JsonConvert.SerializeObject(model);
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), jsonRes,  ex.StackTrace ?? string.Empty);
            }
            return false;
        }


    }
}