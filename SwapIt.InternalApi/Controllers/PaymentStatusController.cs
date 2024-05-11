
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
    public class PaymentStatusController : ControllerBase
    {

        private const string CLASS_NAME = "PaymentStatus";

        private readonly ILogger<PaymentStatusController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.PaymentStatus, PaymentStatusModel> _PaymentStatusService;
        public PaymentStatusController(ILogger<PaymentStatusController> logger, IService<SwapIt.Data.Entities.PaymentStatus, PaymentStatusModel> PaymentStatusService, ILogService logService)
        {
            _logger = logger;
            _PaymentStatusService = PaymentStatusService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<PaymentStatusModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _PaymentStatusService.Queryable();
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
        public async Task<PaymentStatusModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _PaymentStatusService.Find<int>(id);
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
        public async Task<PaymentStatusModel?> Create(PaymentStatusModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _PaymentStatusService.InsertAndReturnModel(model);
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

                _PaymentStatusService.Delete<int>(id);
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
        public async Task<ActionResult> Update(PaymentStatusModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _PaymentStatusService.Update(model);
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