
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
    public class ServiceBookmarkController : ControllerBase
    {

        private const string CLASS_NAME = "ServiceBookmark";

        private readonly ILogger<ServiceBookmarkController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ServiceBookmark, ServiceBookmarkModel> _ServiceBookmarkService;
        public ServiceBookmarkController(ILogger<ServiceBookmarkController> logger, IService<SwapIt.Data.Entities.ServiceBookmark, ServiceBookmarkModel> ServiceBookmarkService, ILogService logService)
        {
            _logger = logger;
            _ServiceBookmarkService = ServiceBookmarkService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ServiceBookmarkModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ServiceBookmarkService.Queryable();
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
        public async Task<ServiceBookmarkModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ServiceBookmarkService.Find<int>(id);
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
        public async Task<ServiceBookmarkModel?> Create(ServiceBookmarkModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ServiceBookmarkService.InsertAndReturnModel(model);
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

                _ServiceBookmarkService.Delete<int>(id);
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
        public async Task<ActionResult> Update(ServiceBookmarkModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ServiceBookmarkService.Update(model);
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