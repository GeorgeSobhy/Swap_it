
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
    public class CountryController : ControllerBase
    {

        private const string CLASS_NAME = "Country";

        private readonly ILogger<CountryController> _logger;
        private readonly ILogService _logService;
        private readonly IService<SwapIt.Data.Entities.Country, CountryModel> _CountryService;
        public CountryController(ILogger<CountryController> logger, IService<SwapIt.Data.Entities.Country, CountryModel> CountryService, ILogService logService)
        {
            _logger = logger;
            _CountryService = CountryService;
            _logService = logService;
        }
        [HttpGet("GetAll")]
        public async Task<List<CountryModel>?> GetAll()
        {

            const string METHOD_NAME = "GetAll";

            try
            {

                return _CountryService.Queryable();
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
        public async Task<CountryModel?> GetById(int Id)
        {

            const string METHOD_NAME = "GetById";

            try
            {

                return _CountryService.Find<int>(Id);
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
        public async Task<CountryModel?> Create(CountryModel model)
        {

            const string METHOD_NAME = "Create";

            try
            {

                return _CountryService.InsertAndReturnModel(model);
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

                _CountryService.Delete<int>(Id);
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
        public async Task<ActionResult> Update(CountryModel model)
        {

            const string METHOD_NAME = "Update";

            try
            {

                _CountryService.Update(model);
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