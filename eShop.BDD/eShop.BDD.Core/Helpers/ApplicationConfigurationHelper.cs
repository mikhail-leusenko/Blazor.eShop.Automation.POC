using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.BDD.Core.Helpers
{
    /// <summary>
    /// Helper class to obtain configuration from appsettings.json file and to set all required fields for further usage in tests.
    /// </summary>
    public class ApplicationConfigurationHelper
    {
        public IConfiguration Configuration { get; set; }
        public string DbType { get; set; }
        public string BrowserName { get; set; }
        public bool IsSecure { get; set; }

        public ApplicationConfigurationHelper()
        {
            this.Configuration = this.GetCurrentConfiguration();
            this.DbType = GetDbType();
            this.BrowserName = GetBrowserName();
            this.IsSecure = GetIsSecureFlag();
        }

        

        /// <summary>
        /// Gets current configuration from 'appsettings.json' file.
        /// </summary>
        /// <returns>Current configuration which is builded from appsettings.json.</returns>
        public IConfiguration GetCurrentConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false, true).Build();
        }

        /// <summary>
        /// Gets selected database hosting type from current configuration.
        /// </summary>
        /// <returns>Database hosting type as a string to init the required database instance for test run.</returns>
        private string GetDbType()
        {
            return this.Configuration.GetValue<string>(@"DbType");
        }

        /// <summary>
        /// Gets selected browser from current configuration.
        /// </summary>
        /// <returns>Browser type as a string to init the appropriate web driver.</returns>
        private string GetBrowserName()
        {
            return this.Configuration.GetValue<string>(@"WebBrowser");
        }

        private bool GetIsSecureFlag()
        {
            return this.Configuration.GetValue<bool>(@"RunWithHttps");
        }
    }
}
