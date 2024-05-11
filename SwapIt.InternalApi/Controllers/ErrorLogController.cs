
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
    public class ErrorLogController : ControllerBase
    {

        private const string CLASS_NAME = "ErrorLog";

        private readonly ILogger<ErrorLogController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.ErrorLog, ErrorLogModel> _ErrorLogService;
        public ErrorLogController(ILogger<ErrorLogController> logger, IService<SwapIt.Data.Entities.ErrorLog, ErrorLogModel> ErrorLogService, ILogService logService)
        {
            _logger = logger;
            _ErrorLogService = ErrorLogService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<ErrorLogModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _ErrorLogService.Queryable();
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
        public async Task<ErrorLogModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _ErrorLogService.Find<int>(id);
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
        public async Task<ErrorLogModel?> Create(ErrorLogModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _ErrorLogService.InsertAndReturnModel(model);
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

                _ErrorLogService.Delete<int>(id);
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
        public async Task<ActionResult> Update(ErrorLogModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _ErrorLogService.Update(model);
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