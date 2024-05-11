
using SwapIt.BusinessLogic.Authentication.Attributes.WebAPI;
using SwapIt.BusinessLogic.Services;
using SwapIt.Mapper.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Pattern;
using SwapIt.Repository.Repositories;
using SwapIt.Data.Entities;
using System.Web.Http;
using Castle.Core.Resource;

namespace SwapIt.Api.Controllers
{
    [Authorize(claim: new[] { "Provider", "Admin" })]
    [ApiController]
    [Route("[controller]")]
    public class CustomerBalanceController : ControllerBase
    {

        private const string CLASS_NAME = "CustomerBalance";

        private readonly ILogger<CustomerBalanceController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.CustomerBalance, CustomerBalanceModel> _CustomerBalanceService;
        private readonly ICCustomerBalanceService _cCustomerBalanceService;
        public CustomerBalanceController(ICCustomerBalanceService cCustomerBalanceService, ILogger<CustomerBalanceController> logger, IService<SwapIt.Data.Entities.CustomerBalance, CustomerBalanceModel> CustomerBalanceService, ILogService logService)
        {
            _logger = logger;
            _CustomerBalanceService = CustomerBalanceService;
            _logService = logService;
            _cCustomerBalanceService = cCustomerBalanceService;
        }
        [HttpGet("GetAll")]
        public async Task<List<CustomerBalanceModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _CustomerBalanceService.Queryable();
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
        public async Task<CustomerBalanceModel?> GetById(int id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _CustomerBalanceService.Find<int>(id);
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
        public async Task<CustomerBalanceModel?> Create(CustomerBalanceModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _CustomerBalanceService.InsertAndReturnModel(model);
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

                _CustomerBalanceService.Delete<int>(id);
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
        public async Task<ActionResult> Update(CustomerBalanceModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _CustomerBalanceService.Update(model);
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



        [HttpPost("Addpoints")]
        public async Task<ActionResult> Addpoints(int customerId, int points)
        {

            const string METHOD_NAME = "Update";
            try
            { 
                var result = _cCustomerBalanceService.Addpoints(customerId, points);
                if (result != null)
                    return StatusCode(200, result);
                else 
                    return StatusCode(400, result);
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