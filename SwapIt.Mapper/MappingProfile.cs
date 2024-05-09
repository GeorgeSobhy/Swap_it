
using SwapIt.Data.Entities;
using SwapIt.Mapper.Models;
using AutoMapper;
using System.Diagnostics.Metrics;

namespace SwapIt.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RoleModel, Role>();
            CreateMap<Role, RoleModel>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<ErrorLogModel, ErrorLog>();
            CreateMap<ErrorLog, ErrorLogModel>();


            CreateMap<ServiceTypeModel, ServiceType>();
            CreateMap<ServiceType, ServiceTypeModel>();

            CreateMap<ServiceModel, Service>();
            CreateMap<Service, ServiceModel>();
            CreateMap<CityModel, City>();
            CreateMap<City, CityModel>();
            CreateMap<CountryModel, Country>();
            CreateMap<Country, CountryModel>();
            CreateMap<CustomerBalanceModel, CustomerBalance>();
            CreateMap<CustomerBalance, CustomerBalanceModel>();
            CreateMap<NotificationModel, Notification>();
            CreateMap<Notification, NotificationModel>();
            CreateMap<PaymentStatusModel, PaymentStatus>();
            CreateMap<PaymentStatus, PaymentStatusModel>();
            CreateMap<ServiceBookmarkModel, ServiceBookmark>();
            CreateMap<ServiceBookmark, ServiceBookmarkModel>();
            CreateMap<ServiceImageModel, ServiceImage>();
            CreateMap<ServiceImage, ServiceImageModel>();
            CreateMap<ServiceRequestModel, ServiceRequest>();
            CreateMap<ServiceRequest, ServiceRequestModel>();
            CreateMap<ServiceStatusModel, ServiceStatus>();
            CreateMap<ServiceStatus, ServiceStatusModel>();

        }
    }
}
