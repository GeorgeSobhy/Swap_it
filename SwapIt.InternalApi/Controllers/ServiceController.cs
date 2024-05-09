
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
    public class ServiceController : ControllerBase
    {

        private const string CLASS_NAME = "Service";

        private readonly ILogger<ServiceController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.Service, ServiceModel> _ServiceService;
        public ServiceController(ILogger<ServiceController> logger, IService<SwapIt.Data.Entities.Service, ServiceModel> ServiceService, ILogService logService)
        {
            _logger = logger;
            _ServiceService = ServiceService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ServiceModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ServiceService.Queryable();
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
        public async Task<ServiceModel?> GetById(int Id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ServiceService.Find<int>(Id);
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
        public async Task<ServiceModel?> Create(ServiceModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ServiceService.InsertAndReturnModel(model);
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

                _ServiceService.Delete<int>(Id);
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
        public async Task<ActionResult> Update(ServiceModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ServiceService.Update(model);
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