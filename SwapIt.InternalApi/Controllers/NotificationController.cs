
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
    public class NotificationController : ControllerBase
    {

        private const string CLASS_NAME = "Notification";

        private readonly ILogger<NotificationController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.Notification, NotificationModel> _NotificationService;
        public NotificationController(ILogger<NotificationController> logger, IService<SwapIt.Data.Entities.Notification, NotificationModel> NotificationService, ILogService logService)
        {
            _logger = logger;
            _NotificationService = NotificationService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<NotificationModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _NotificationService.Queryable();
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
        public async Task<NotificationModel?> GetById(int Id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _NotificationService.Find<int>(Id);
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
        public async Task<NotificationModel?> Create(NotificationModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _NotificationService.InsertAndReturnModel(model);
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

                _NotificationService.Delete<int>(Id);
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
        public async Task<ActionResult> Update(NotificationModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _NotificationService.Update(model);
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