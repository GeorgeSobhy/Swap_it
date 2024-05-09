using SwapIt.BusinessLogic.Authentication.Attributes.WebAPI;
using SwapIt.BusinessLogic.Services;
using SwapIt.Data.Entities;
using SwapIt.Mapper.Models;
using SwapIt.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Service.Pattern;

namespace SwapIt.Api.Controllers
{
    [Authorize(claim: new[] { "Provider", "Admin", "Customer" })]
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private const string CLASS_NAME = "TestController";

        private readonly ILogger<TestController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.Service, ServiceModel> _serviceService;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IService<User, UserModel> _userService;
        public TestController(IService<User, UserModel> userService, IService<SwapIt.Data.Entities.Service, ServiceModel> serviceService, IProcedureRepository procedureRepository, ILogService logService)
        {
            _serviceService = serviceService;
            _logService = logService;
            _procedureRepository = procedureRepository;
            _userService = userService;
        }

        [HttpGet("test")]
        public async Task<UserModel?> test()
        {

            const string METHOD_NAME = "test";

            try
            {

                return _userService.QueryFirstModel(x => x.Id == 1);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(),  ex.Message, ex.StackTrace ?? string.Empty);

            }
            return null;
        }


    }
}