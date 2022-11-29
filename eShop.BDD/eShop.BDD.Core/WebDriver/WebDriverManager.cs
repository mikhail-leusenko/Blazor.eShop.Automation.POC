using eShop.BDD.Core.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.WebDriver
{
    /// <summary>
    /// Provides necessary methods to manage the web driver and related browser instances.
    /// </summary>
    public class WebDriverManager
    {
        private readonly FeatureContext FeatureContext;
        private readonly ApplicationConfigurationHelper ConfigurationHelper;
        private readonly string BrowserName;

        /// <summary>
        /// Instance of the WebDriver.
        /// </summary>
        public IWebDriver Driver { get; private set; }

        public WebDriverManager(FeatureContext featureContext, ApplicationConfigurationHelper applicationConfigurationHelper)
        {
            this.FeatureContext = featureContext;
            this.ConfigurationHelper = applicationConfigurationHelper;
            this.BrowserName = this.ConfigurationHelper.BrowserName;
        }

        /// <summary>
        /// Inits the required web driver depending on the appropriate data specified in the appsettings.json.
        /// Sets the web driver to the FeatureContext in order to use the same instance of browser for all tests in feature file.
        /// </summary>
        /// <returns>The instance of the WebDriver.</returns>
        public IWebDriver InitDriver()
        {
            switch (this.BrowserName)
            {
                case "Chrome":
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--allow-insecure-localhost");
                        chromeOptions.AddArgument("disable-popup-blocking");
                        chromeOptions.AddArgument("--disable-notifications");
                        chromeOptions.AddArgument("--incognito");

                        this.Driver = new ChromeDriver(chromeOptions);
                        this.Driver.Manage().Window.Maximize();

                        this.FeatureContext.Set(this.Driver);
                        this.Driver.Manage().Cookies.DeleteAllCookies();

                        return this.Driver;
                    }
                case "Edge":
                    {
                        this.Driver = new EdgeDriver();
                        this.Driver.Manage().Window.Maximize();
                        this.FeatureContext.Set(this.Driver);
                        this.Driver.Manage().Cookies.DeleteAllCookies();
                        return this.Driver;
                    }
                default:
                    {
                        throw new NotSupportedException($"{this.BrowserName} is not supported.");
                    }
            }
        }

        /// <summary>
        /// Refreshes the state of browser window after the test execution was completed.
        /// </summary>
        public void RefreshBrowserState()
        {
            this.Driver.Manage().Cookies.DeleteAllCookies();
            //this.Driver.Navigate().GoToUrl(this.FeatureContext.Get<ApplicationConfigurationHelper>().GetApplicationUrl());
        }

        /// <summary>
        /// Disposes the current instance of the WebDriver.
        /// </summary>
        /// <param name="featureContext">The IWebDriver is stored in Feature Context, so it should be called prior to disposing.</param>
        public void DisposeDriver(FeatureContext featureContext)
        {
            var driver = featureContext.Get<IWebDriver>();
            driver.Quit();
            driver.Dispose();
        }
    }
}
