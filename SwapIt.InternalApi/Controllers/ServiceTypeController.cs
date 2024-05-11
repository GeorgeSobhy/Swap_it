
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
        [HttpGet("GetAll")]
        public async Task<List<ServiceTypeModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

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


        [HttpGet("GetById")]
        public async Task<ServiceTypeModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _serviceTypeService.Find<int>(id);
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
        public async Task<ServiceTypeModel?> Create(ServiceTypeModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _serviceTypeService.InsertAndReturnModel(model);
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
        public async Task<ActionResult> Delete(int id)
        {

            const string METHOD_NAME = "Delete";

            try
            {

                _serviceTypeService.Delete<int>(id);
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
        public async Task<ActionResult> Update(ServiceTypeModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _serviceTypeService.Update(model);
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