
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
    public class ServiceRequestController : ControllerBase
    {

        private const string CLASS_NAME = "ServiceRequest";

        private readonly ILogger<ServiceRequestController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ServiceRequest, ServiceRequestModel> _ServiceRequestService;
        public ServiceRequestController(ILogger<ServiceRequestController> logger, IService<SwapIt.Data.Entities.ServiceRequest, ServiceRequestModel> ServiceRequestService, ILogService logService)
        {
            _logger = logger;
            _ServiceRequestService = ServiceRequestService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ServiceRequestModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ServiceRequestService.Queryable();
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
        public async Task<ServiceRequestModel?> GetById(int Id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ServiceRequestService.Find<int>(Id);
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
        public async Task<ServiceRequestModel?> Create(ServiceRequestModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ServiceRequestService.InsertAndReturnModel(model);
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

                _ServiceRequestService.Delete<int>(Id);
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
        public async Task<ActionResult> Update(ServiceRequestModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ServiceRequestService.Update(model);
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