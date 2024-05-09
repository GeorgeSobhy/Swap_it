
using SwapIt.BusinessLogic.Authentication.Attributes.WebAPI;
using SwapIt.BusinessLogic.Services;
using SwapIt.Mapper.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Pattern;
using SwapIt.Repository.Repositories;
using SwapIt.Data.Entities;
using System.Web.Http;

namespace SwapIt.Api.Controllers
{
    [Authorize(claim: new[] { "Provider", "Admin" })]
    [ApiController]
    [Route("[controller]")]
    public class ServiceStatusController : ControllerBase
    {

        private const string CLASS_NAME = "ServiceStatus";

        private readonly ILogger<ServiceStatusController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ServiceStatus, ServiceStatusModel> _ServiceStatusService;
        public ServiceStatusController(ILogger<ServiceStatusController> logger, IService<SwapIt.Data.Entities.ServiceStatus, ServiceStatusModel> ServiceStatusService, ILogService logService)
        {
            _logger = logger;
            _ServiceStatusService = ServiceStatusService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ServiceStatusModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ServiceStatusService.Queryable();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);

            }
            return null;
        }


        [HttpGet("GetById")]
        public async Task<ServiceStatusModel?> GetById(int Id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ServiceStatusService.Find<int>(Id);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);

            }
            return null;
        }



        [HttpPost("Create")]
        public async Task<ServiceStatusModel?> Create(ServiceStatusModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ServiceStatusService.InsertAndReturnModel(model);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);

            }
            return null;
        }



        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int Id)
        {

            const string METHOD_NAME = "Delete";

            try
            {

                _ServiceStatusService.Delete<int>(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);
                return StatusCode(500, ex.ToString());
            }

        }


        [HttpPost("Update")]
        public async Task<ActionResult> Update(ServiceStatusModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ServiceStatusService.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                _logService.AddErrorLog(CLASS_NAME, METHOD_NAME, ex.ToString(), ex.Message, ex.StackTrace ?? string.Empty);
                return StatusCode(500, ex.ToString());

            }

        }


    }
}