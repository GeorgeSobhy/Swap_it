using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RquestContext.Configuration
{
    public static class ConfigurationHelper
    {
      
        public static ConfigurationValuesModel? ConfigurationValues;
        public static void Initialize(IConfiguration Configuration)
        {
            ConfigurationValues = Configuration.GetSection(ConfigurationValuesModel.ValuesName).Get<ConfigurationValuesModel>();
            
        }
    }
}
