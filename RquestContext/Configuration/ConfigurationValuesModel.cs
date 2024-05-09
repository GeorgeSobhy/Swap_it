 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RquestContext.Configuration
{
    public class ConfigurationValuesModel
    {
        public const string ValuesName = "Values";

        public string AppName { get; set; } = String.Empty;
        public string BuildNumber { get; set; } = String.Empty;
        public string Stage { get; set; } = String.Empty;
        public string WEBSITE_SITE_NAME { get; set; } = String.Empty;

        public string TokenIssuer { get; set; } = "SwapIt API";
        public string TokenKey { get; set; } = "any@valuesss@cool@2000@08@2222876@home";

    }
}
