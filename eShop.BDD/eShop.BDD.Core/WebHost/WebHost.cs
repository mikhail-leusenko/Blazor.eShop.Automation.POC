using eShop.BDD.Core.WebHost.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace eShop.BDD.Core.WebHost
{
    public class WebHost
    {
        private readonly IPortHelper PortHelper;
        private readonly IWebHostManager WebHostManager;

        public IServiceProvider WebHostServiceProvider;
        public WebHost(IWebHostConfiguration webHostConfiguration) :
            this(new PortHelper(webHostConfiguration.Port),
                 new WebHostManager(webHostConfiguration))
        { }

        internal WebHost(IPortHelper portHelper, IWebHostManager webHostManager)
        {
            this.PortHelper = portHelper;
            this.WebHostManager = webHostManager;
        }

        public void StartWebServer<T>(IServiceCollection services, int nextAvailablePort) where T : class
        {
            this.WebHostManager.Start<T>(nextAvailablePort, services);
            this.WebHostServiceProvider = this.WebHostManager.SetServiceProvider();
        }

        /// <summary>
        /// Starts a web server on an available port with the supplied generic as the startup type.
        /// </summary>
        public void StartWebServer<T>() where T : class
        {
            var nextAvailablePort = this.PortHelper.GetFreeTcpPort();
            this.WebHostManager.Start<T>(nextAvailablePort);
        }

        /// <summary>
        /// Stops the running web server and disposes of it.
        /// </summary>
        public void StopWebServer()
        {
            this.WebHostManager.Stop();
        }

        /// <summary>
        /// Returns the address of the running web server as a string.
        /// </summary>
        public string GetServerAddress()
        {
            return this.WebHostManager.GetHostAddress();
        }
    }
}