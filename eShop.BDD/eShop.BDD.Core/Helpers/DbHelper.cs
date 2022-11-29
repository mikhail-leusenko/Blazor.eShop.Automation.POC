using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Helpers
{
    /// <summary>
    /// Class which supposed to generate the connetcion string based on Feature which is currently running.
    /// </summary>
    public class DbHelper
    {
        public string DbConnectionString;
        private FeatureContext FeatureContext { get; }
        public DbHelper(FeatureContext featureContext)
        {
            this.FeatureContext = featureContext;
        }

        /// <summary>
        /// Generates DB Connection String based on feature and adds it to the FeatureContext under the appropriate Key.
        /// </summary>
        public void SetConnectionStringByFeature()
        {
            this.DbConnectionString = $"Server =.\\SQLEXPRESS;Database=BlazorShopDb_{this.FeatureContext.FeatureInfo.Title};Trusted_Connection=True;MultipleActiveResultSets=true";
            //this.FeatureContext.Add($"{this.FeatureContext.FeatureInfo.Title}ConnectionString", this.DbConnectionString);
        }

        ///// <summary>
        ///// Sets the Database instance for the test run either in memory or via SQL Server based on the type which should be specified in appsettings.json.
        ///// </summary>
        ///// <param name="dbType">String representation of the desired DB Type to use in test run.</param>
        //public void SetDbTestInstance()
        //{
        //    var serviceCollection = this.FeatureContext.Get<IServiceCollection>();

        //    var dbType = this.FeatureContext.Get<ApplicationConfigurationHelper>().DbType;

        //    switch (dbType.Trim().ToLower())
        //    {
        //        case "inmemory":
        //            // Use in-memory database based on Feature Title
        //            serviceCollection.AddDbContext<AspnetRunContext>(c =>
        //                c.UseInMemoryDatabase(this.FeatureContext.FeatureInfo.Title));
        //            break;

        //        case "sqldb":
        //            // Use SQL Server hosted database based on Connection String which was generated based on Feature Title
        //            this.SetConnectionStringByFeature();
        //            serviceCollection.AddDbContext<AspnetRunContext>(c =>
        //                c.UseSqlServer(this.DbConnectionString, x => x.MigrationsAssembly("eShop.Web")));
        //            break;

        //        default:
        //            throw new NotImplementedException($"The specified \"{dbType}\" DB type is not supported by current implementation." +
        //                                              $"{Environment.NewLine}Check the appsettings.json DbType value.");
        //    }
        //}
    }
}
