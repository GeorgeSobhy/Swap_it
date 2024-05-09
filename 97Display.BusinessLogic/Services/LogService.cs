using SwapIt.Data.Entities;
using RquestContext.Configuration;
using SwapIt.Mapper.Models;
using Microsoft.Extensions.Options;
using Service.Pattern;

namespace SwapIt.BusinessLogic.Services
{
    public class LogService : ILogService
    {

        private readonly ConfigurationValuesModel _configurationValuesModel;
        private readonly IService<ErrorLog, ErrorLogModel> _errorLogService;
        public LogService( IOptions<ConfigurationValuesModel> configurationValuesModel, IService<ErrorLog, ErrorLogModel> errorLogService)
        {
            _configurationValuesModel = configurationValuesModel.Value;
            _errorLogService = errorLogService; 
        }

        
        public void AddErrorLog(string className, string methodName, string errorMsg, string errorCode, string stackTrace, DateTime? dateAdded = null)
        {

            if (dateAdded == null)
            {
                dateAdded = DateTime.UtcNow;
            }
            _errorLogService.Insert(new ErrorLogModel() { ClassName = className, DateAdded = dateAdded.Value, ErrorCode = errorCode, ErrorMsg = errorMsg, MethodName = methodName, StackTrace = stackTrace });
            
        }

        public void AddErrorLog(ErrorLogModel model)
        {
          
            _errorLogService.Insert(model);

        } 
    }
    public interface ILogService
    {
        void AddErrorLog(string className, string methodName, string errorMsg, string errorCode, string stackTrace, DateTime? dateAdded = null);
         
        void AddErrorLog(ErrorLogModel model); 

    }
}

