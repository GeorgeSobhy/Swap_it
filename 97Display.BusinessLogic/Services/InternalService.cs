using SwapIt.Data.Entities;
using RquestContext.Configuration;
using SwapIt.Mapper.Infrastructure;
using SwapIt.Mapper.Models;
using SwapIt.Mapper.Models.Infrastructure;
using SwapIt.Repository.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using Service.Pattern;
namespace SwapIt.BusinessLogic.Services
{
    public class APIService : IAPIService
    {
        private const String CLASS_NAME = "APIService";

        private readonly IService<User, UserModel> _userService;
        private readonly ILogService _logService;

        public APIService(ILogService logService, IService<User, UserModel> userService)
        {
            _logService = logService;
            _userService = userService;
        }

    }

    public interface IAPIService
    {


    }


}
