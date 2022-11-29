using eShop.BDD.Core.WebHost.Interfaces;

namespace eShop.BDD.Core.WebHost
{
    public class WebHostConfiguration : IWebHostConfiguration
    {
        public string ApplicationPath { get; set; }

        public string AssemblyName { get; set; }

        public int Port { get; set; }

        public bool IsSecure { get; set; }
    }
}
