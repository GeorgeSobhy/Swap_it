using SwapIt.BusinessLogic.Authentication;
using SwapIt.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using SwapIt.Data;
using SwapIt.Repository.Repositories;
using ProcedureRepository.Pattern.EF;
using Repository.Pattern.EF;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork; 

namespace SwapIt.BusinessLogic
{

    public static class MyConfigServiceCollectionExtensions
    {
        // These common High level business logic services called from all APPs Web, and APIs
        public static IServiceCollection AddServices(
             this IServiceCollection services)
        {

            services.AddScoped<IDataContextAsync, ApplicationContext>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRawSqlRepository), typeof(RawSqlRepository));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(Repository<>));
            services.AddScoped<IProcedureRepository, SwapIt.Repository.Repositories.ProcedureRepository>();
            services.AddScoped(typeof(Service.Pattern.IService<,>), typeof(Service.Pattern.Service<,>));
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IInternalService, InternalService>();
            

            return services;
        }

   
        public static IServiceCollection AddProviders(
          this IServiceCollection s)
        {

            

            return s;
        }

    }
}
