
using SwapIt.BusinessLogic.Authentication.Attributes.WebAPI;
using SwapIt.BusinessLogic.Services;
using SwapIt.Mapper.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Pattern;
using SwapIt.Repository.Repositories;
using SwapIt.Data.Entities;

namespace SwapIt.Api.Controllers
{
    [Authorize(claim: new[] { "Provider", "Admin", "Customer" })]
    [ApiController]
    [Route("[controller]")]
    public class ServiceTypeController : ControllerBase
    {

        private const string CLASS_NAME = "ServiceType";

        private readonly ILogger<ServiceTypeController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ServiceType, ServiceTypeModel> _serviceTypeService;
        public ServiceTypeController(ILogger<ServiceTypeController> logger, IService<SwapIt.Data.Entities.ServiceType, ServiceTypeModel> serviceTypeService, ILogService logService)
        {
            _logger = logger;
            _serviceTypeService = serviceTypeService;
            _logService = logService;
        }
        [HttpGet("GetAllServiceTypes")]
        public async Task<List<ServiceTypeModel>?> GetAllServiceTypes()
        {

            const string METHOD_NAME = "GetAllServiceTypes";

            try
            {

                return _serviceTypeService.Queryable();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);

            }
            return null;
        }


    }
}