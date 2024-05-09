using SwapIt.Data.Entities;
using RquestContext.Configuration;
using SwapIt.Mapper.Infrastructure;
using SwapIt.Mapper.Models;
using SwapIt.Mapper.Models.Infrastructure;
using SwapIt.Repository.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using Service.Pattern;
using Microsoft.AspNetCore.Mvc;
namespace SwapIt.BusinessLogic.Services
{
    public class CCustomerBalanceService : ICCustomerBalanceService
    {
        private const String CLASS_NAME = "CCustomerBalanceService";

        private readonly IService<SwapIt.Data.Entities.CustomerBalance, CustomerBalanceModel> _customerBalanceService;
        private readonly ILogService _logService;

        public CCustomerBalanceService(ILogService logService, IService<SwapIt.Data.Entities.CustomerBalance, CustomerBalanceModel> customerBalanceService)
        {
            _logService = logService;
            _customerBalanceService = customerBalanceService;
        }
        public CustomerBalanceModel? Addpoints(int customerId, int points)
        {
            var _userBalance = _customerBalanceService.QueryFirstModel(x => x.CustomerId == customerId);
            if (_userBalance != null)
            {
                _userBalance.Points = _userBalance.Points + points;
                _customerBalanceService.Update(_userBalance);
                return _userBalance;
            }
            else
            {
                return null;
            }
        }


    }

    public interface ICCustomerBalanceService
    {
        CustomerBalanceModel Addpoints(int customerId, int points);

    }


}
