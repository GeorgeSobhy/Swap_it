using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RquestContext.Configuration
{
    public class AppConfigurationModel
    {
        public const string AppConfigurationName = "AppSettings";
         
        public string AppName { get; set; } = String.Empty;
        public string BuildNumber { get; set; } = String.Empty;
        public string Stage { get; set; } = String.Empty;
    }
}
