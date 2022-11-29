namespace eShop.BDD.Core.WebHost.Interfaces
{
    public interface IWebHostConfiguration
    {
        /// <summary>
        /// Path to the web application root.
        /// </summary>
        string ApplicationPath { get; }

        /// <summary>
        /// Name of the application assembly.
        /// </summary>
        string AssemblyName { get; }
        
        /// <summary>
        /// Port number which should be used for Web Application start up.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Flag which indicates either the application should use HTTPS or not.
        /// </summary>
        bool IsSecure { get; }
    }
}