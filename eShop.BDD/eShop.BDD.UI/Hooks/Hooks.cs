using eShop.BDD.Core.Helpers;
using eShop.BDD.Core.WebDriver;
using eShop.BDD.Core.WebHost;
using eShop.BDD.Core.WebHost.Interfaces;
using eShop.Web;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.BDD.Core.Data;
using System.Linq;
using System.Threading.Tasks;
using eShop.BDD.Core.Steps;
using OpenQA.Selenium;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace eShop.BDD.UI.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
        }

        [BeforeFeature(Order = 0)]
        public static void SetDefaultServiceCollection(FeatureContext featureContext)
        {
            featureContext.Set<IServiceCollection>(new ServiceCollection());
        }

        [BeforeFeature(Order = 0)]
        public static void SetRepositoriesForServices(FeatureContext featureContext)
        {
            SetRepositoriesForServiceCollection(featureContext);
        }


        [BeforeFeature(Order = 1)]
        public static void InitInstances(FeatureContext featureContext)
        {
            SetStaticInstances(featureContext);

            StartHttpServer(featureContext);

            Console.WriteLine("Instances for hooks initiated.");
        }

        [BeforeFeature(Order = 1)]
        public static void InitDriverInstance(FeatureContext featureContext)
        {
            var webDriverManager = featureContext.Get<WebDriverManager>();

            webDriverManager.InitDriver();

            webDriverManager.Driver
                .Navigate()
                .GoToUrl(Environment.GetEnvironmentVariable("applicationUrl"));

            Console.WriteLine("Web Driver instance initiated.");
        }

        [AfterScenario]
        public static void AfterScenario(FeatureContext featureContext)
        {
            featureContext.Get<TestOrderRepository>().CleanUp();
        }

        /// <summary>
        /// Step which performs the deletion of the products in cart after it was put there during the tests execution.
        /// Needs in order to perfrom a dry run of the further tests.
        /// Such implementation has been selected due to the impossibility to interact with cart via backend,
        /// so far as it uses JSRunTime which supposed to be called from the blazor pages
        /// and the Cart data is stored in runtime.
        /// </summary>
        /// <param name="featureContext">FeatureContext instance to obtain BaseSteps and IWebDriver instances. </param>
        /// <param name="scenarioContext">FeatureContext instance to obtain BaseSteps instance. </param>
        [AfterScenario]
        [Scope(Tag = "Service_SetCart")]
        public async Task CleanupCart(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var baseSteps = new BaseSteps(scenarioContext, featureContext);

            await baseSteps.NavigateToPageByName("Cart");

            var driver = featureContext.Get<WebDriverManager>().Driver;

            try
            {
                PerformCartCleanUp(driver);
            }
            catch (StaleElementReferenceException)
            {
                driver.Navigate().Refresh();

                PerformCartCleanUp(driver);
            }
        }

        [AfterFeature]
        public static void DisposeDrverInstance(FeatureContext featureContext)
        {
            featureContext.Get<WebDriverManager>().DisposeDriver(featureContext);
        }

        private void PerformCartCleanUp(IWebDriver webDriver)
        {
            webDriver
               .FindElements(By.CssSelector("ul li.row div > button.btn-delete"))
               .ToList<IWebElement>()
               .ForEach(x => x.Click());
        }

        private static void SetStaticInstances(FeatureContext featureContext)
        {
            featureContext.Set(new ApplicationConfigurationHelper());
            featureContext.Set(new WebDriverManager(featureContext, featureContext.Get<ApplicationConfigurationHelper>()));
            featureContext.Set<IPortHelper>(new PortHelper(ObtainTcpPortFromFeatureTag(featureContext)));
            featureContext.Set(SetupWebHostConfiguration(featureContext));
        }

        private static IWebHostConfiguration SetupWebHostConfiguration(FeatureContext featureContext)
        {
            var hostConfiguration = new WebHostConfiguration
            {
                ApplicationPath = "eShop.Web",
                AssemblyName = typeof(Program).GetTypeInfo().Assembly.FullName,
                Port = featureContext.Get<IPortHelper>().GetFreeTcpPort(),
                IsSecure = featureContext.Get<ApplicationConfigurationHelper>().IsSecure
            };

            return hostConfiguration;
        }

        private static void StartHttpServer(FeatureContext featureContext)
        {
            var hostConfiguration = featureContext.Get<IWebHostConfiguration>();

            var webHost = new WebHost(hostConfiguration);

            // Obtain the free TCP port for WebHost instance, create the application URL and place it to Environment variables for future uses.
            Environment.SetEnvironmentVariable("applicationUrl", GenerateWebHostUrl(hostConfiguration.Port));

            webHost.StartWebServer<Startup>(featureContext.Get<IServiceCollection>(), hostConfiguration.Port);
            featureContext.Set(webHost);
        }

        private static string GenerateWebHostUrl(int nextAvailablePort)
        {
            return new string("https://localhost:" + nextAvailablePort.ToString());
        }

        private static int ObtainTcpPortFromFeatureTag(FeatureContext featureContext)
        {
            int startingPort = 1025;

            foreach (var tag in featureContext.FeatureInfo.Tags)
            {
                if (tag.Contains("StartingPort"))
                {
                    startingPort = Int32.Parse(Regex.Match(tag, @"\d+").Value);
                    break;
                }
            }

            return startingPort;
        }

        private static void SetRepositoriesForServiceCollection(FeatureContext featureContext)
        {
            // set TestProductRepository 
            featureContext.Set<IProductRepository>(new TestProductRepository());
            featureContext.Get<IServiceCollection>().AddSingleton<IProductRepository, TestProductRepository>();

            // set TestOrderRepository
            featureContext.Set(new TestOrderRepository());
            featureContext.Get<IServiceCollection>().AddSingleton<IOrderRepository, TestOrderRepository>();
        }
    }
}