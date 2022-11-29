using eShop.BDD.Core.WebHost.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace eShop.BDD.Core.WebHost
{
    /// <summary>
    /// Implementation of Web Host Manager to create a web host with target application.
    /// </summary>
    internal class WebHostManager : IWebHostManager
    {
        private IWebHostConfiguration WebHostConfiguration;
        private IWebHost WebHost;


        public WebHostManager(IWebHostConfiguration webHostConfiguration)
        {
            if (webHostConfiguration == null)
            {
                throw new ArgumentNullException(nameof(webHostConfiguration), "Parameter must not be null.");
            }

            if (string.IsNullOrWhiteSpace(webHostConfiguration.ApplicationPath))
            {
                throw new ArgumentException("Parameter must not be null or whitespace.", nameof(webHostConfiguration.ApplicationPath));
            }

            if (string.IsNullOrWhiteSpace(webHostConfiguration.AssemblyName))
            {
                throw new ArgumentException("Parameter must not be null or whitespace.", nameof(webHostConfiguration.AssemblyName));
            }

            this.WebHostConfiguration = new WebHostConfiguration
            {
                ApplicationPath = webHostConfiguration.ApplicationPath,
                AssemblyName = webHostConfiguration.AssemblyName,
                IsSecure = webHostConfiguration.IsSecure,
                Port = webHostConfiguration.Port
            };
        }

        /// <summary>
        /// Gets the URL address of the WebHost.
        /// </summary>
        /// <returns>String representation of the WebHost URL address. </returns>
        public string GetHostAddress()
        {
            if (this.WebHost == null)
            {
                throw new InvalidOperationException("The web server is not running. " +
                    "You must call StartWebServer before trying to access the address of the running web server.");
            }
            try
            {
                return this.WebHost.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            }
            catch (NullReferenceException exception)
            {
                throw new NullReferenceException($"Exception has been caught on getting the web host address.{Environment.NewLine}" +
                    $"The original exception message is:{Environment.NewLine}{exception.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong while obtaining the Host URL.{Environment.NewLine}" +
                    $"The unhandled exception appears.{Environment.NewLine}" +
                    $"The original exception message is:{Environment.NewLine}{ex.Message}");
            }
        }

        /// <summary>
        /// Starts the application with injected service collection.
        /// </summary>
        /// <typeparam name="T">Instance of the Startup class. </typeparam>
        /// <param name="startupTcpPort">TCP port which should be used to start the WebHost. </param>
        /// <param name="services">Service Collection configured via Dependency Injection. </param>
        public void Start<T>(int startupTcpPort, IServiceCollection services) where T : class
        {
            if (this.WebHost == null)
            {
                if (this.WebHost == null)
                    this.WebHost = new WebHostBuilder()
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config
                            .AddJsonFile("appsettings.json")
                            //.AddJsonFile("appsettings.Development.json")
                            .AddEnvironmentVariables();
                    })
                    .UseKestrel(kestrelOptions =>
                    {
                        kestrelOptions.Listen(IPAddress.Loopback, this.WebHostConfiguration.Port, listenOptions =>
                        {
                            if (this.WebHostConfiguration.IsSecure)
                            {
                                listenOptions.Protocols = HttpProtocols.Http1;
                                listenOptions.UseHttps();
                            }
                        });
                    })
                    .ConfigureServices(s =>
                    {
                        foreach (var service in services)
                        {
                            s.Add(service);
                        }
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddDebug();
                    })
                    .UseContentRoot(GetApplicationPath())
                    .UseStartup<T>()
                    .UseSetting(WebHostDefaults.ApplicationKey, this.WebHostConfiguration.AssemblyName)
                    
                    .Build();
            }

            SetServiceProvider();

            this.WebHost.Start();
        }

        /// <summary>
        /// Sets service provider in order to access the services implementations from the code.
        /// </summary>
        /// <returns>ServiceProvider instance. </returns>
        public IServiceProvider SetServiceProvider()
        {
            if(this.WebHost != null)
            {
                return this.WebHost.Services;
            }
            else
            {
                throw new InvalidOperationException("The web server is not running. " +
                    "You must call StartWebServer before trying to access WebHost services.");
            }
            
        }

        /// <summary>
        /// Starts the application with empty service collection.
        /// </summary>
        /// <typeparam name="T">Instance of the Startup class. </typeparam>
        /// <param name="startupTcpPort">TCP port which should be used to start the WebHost. </param>
        public void Start<T>(int startupTcpPort) where T : class
        {
            var services = new ServiceCollection();
            Start<T>(startupTcpPort, services);
        }
        
        /// <summary>
        /// Stops and disposes the instance of Web Host.
        /// </summary>
        public void Stop()
        {
            if (this.WebHost == null)
            {
                return;
            }

            this.WebHost.StopAsync();
            this.WebHost.Dispose();
        }

        /// <summary>
        /// Gets application path to run the web host with it.
        /// </summary>
        /// <returns>Combined path of solution rot directory and application path inside it. </returns>
        private string GetApplicationPath()
        {
            return Path.GetFullPath(Path.Combine(GetSolutionRootPath(), this.WebHostConfiguration.ApplicationPath));
        }

        /// <summary>
        /// Gets solution root path to obtain the working directory.
        /// </summary>
        /// <returns>Path to the solution root directory. </returns>
        private string GetSolutionRootPath()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory.FullName;
        }
    }
}
