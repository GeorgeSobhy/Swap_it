
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
    public class ServiceImageController : ControllerBase
    {

        private const string CLASS_NAME = "ServiceImage";

        private readonly ILogger<ServiceImageController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ServiceImage, ServiceImageModel> _ServiceImageService;
        public ServiceImageController(ILogger<ServiceImageController> logger, IService<SwapIt.Data.Entities.ServiceImage, ServiceImageModel> ServiceImageService, ILogService logService)
        {
            _logger = logger;
            _ServiceImageService = ServiceImageService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ServiceImageModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ServiceImageService.Queryable();
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
        public async Task<ServiceImageModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ServiceImageService.Find<int>(id);
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
        public async Task<ServiceImageModel?> Create(ServiceImageModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ServiceImageService.InsertAndReturnModel(model);
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

                _ServiceImageService.Delete<int>(id);
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
        public async Task<ActionResult> Update(ServiceImageModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ServiceImageService.Update(model);
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