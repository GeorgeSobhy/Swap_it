using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace SwapIt.Api
{
    public class LogDetailsResultFilter : IResultFilter
    {
        private readonly ILogger<LogDetailsResultFilter> _log;

        public LogDetailsResultFilter(ILogger<LogDetailsResultFilter> log)
        {
            _log = log;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            if ((context.HttpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest 
                //||context.HttpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError
                )
                && context.ModelState.ErrorCount > 0)
            {
                var errors = new SerializableError(context.ModelState);
                _log.LogError($"Bad Request Model State Errors: {JsonConvert.SerializeObject(errors)}");
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
