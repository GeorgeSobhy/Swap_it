using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RquestContext.Configuration
{

    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppConfigurationModel>(config.GetSection(AppConfigurationModel.AppConfigurationName));
            services.Configure<ConfigurationValuesModel>(config.GetSection(ConfigurationValuesModel.ValuesName));
            config.GetSection(ConfigurationValuesModel.ValuesName).Get<ConfigurationValuesModel>(); 
            ConfigurationHelper.Initialize(config);
            return services;
        }
        public static IServiceCollection AddHostConfig(
             this IServiceCollection services , string filename )
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder(); 
            configurationBuilder.AddJsonFile(filename);
            IConfiguration config = configurationBuilder.Build();
             
            services.Configure<AppConfigurationModel>(config.GetSection(AppConfigurationModel.AppConfigurationName));
            services.Configure<ConfigurationValuesModel>(config.GetSection(ConfigurationValuesModel.ValuesName));
            ConfigurationHelper.Initialize(config);
            return services;
        }

        public static IServiceCollection AddValuesConfig(
             this IServiceCollection services, IConfiguration config)
        {
             services.Configure<ConfigurationValuesModel>(config.GetSection(ConfigurationValuesModel.ValuesName));
            ConfigurationHelper.Initialize(config);
            return services;
        }
    }
}
