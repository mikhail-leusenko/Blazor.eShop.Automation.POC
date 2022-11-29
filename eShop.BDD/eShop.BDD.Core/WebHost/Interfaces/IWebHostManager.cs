using Microsoft.Extensions.DependencyInjection;
using System;

namespace eShop.BDD.Core.WebHost.Interfaces
{
    internal interface IWebHostManager
    {

        void Start<T>(int startupTcpPort, IServiceCollection services) where T : class;

        void Start<T>(int startupTcpPort) where T : class;

        void Stop();

        IServiceProvider SetServiceProvider();

        string GetHostAddress();
    }
}