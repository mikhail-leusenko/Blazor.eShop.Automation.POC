using eShop.BDD.Core.Data;
using eShop.BDD.Core.Helpers;
using eShop.BDD.Core.Pages.Base;
using eShop.BDD.Core.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Steps
{
    [Binding]
    public class BaseSteps : TechTalk.SpecFlow.Steps
    {
        private new ScenarioContext ScenarioContext { get; }
        private new FeatureContext FeatureContext { get; }
        public IWebDriver Driver { get; }
        public ApplicationConfigurationHelper ConfigurationHelper { get; }
        public string ApplicationUrl { get; }
        public WebElementWrapper Wrapper { get; }

        public BaseSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.ScenarioContext = scenarioContext;
            this.FeatureContext = featureContext;
            this.Driver = this.FeatureContext.Get<IWebDriver>();
            this.ConfigurationHelper = this.FeatureContext.Get<ApplicationConfigurationHelper>();
            this.ApplicationUrl = Environment.GetEnvironmentVariable("applicationUrl"); //TO_DO: obtain appUrl from configurationHelper
            this.Wrapper = new WebElementWrapper(this.FeatureContext);
        }

        /// <summary>
        /// Gets single element on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes). </param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes). </param>
        /// <param name="pageName">Page Name (see PageName attribute values in PageStorage.cs). </param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs). </param>
        /// <returns>Returns the IWebElement that matches the specified parameters. </returns>
        public IWebElement GetElementOnPage(string elementName, string pageName, string elementType = "", string pageType = "page")
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElement(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Gets the collection of similar elements on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes). </param>
        /// <param name="pageName">Element Type (see ElementName attribute values in pages classes). </param>
        /// <param name="elementType">Page Name (see PageName attribute values in PageStorage.cs). </param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs). </param>
        /// <returns>Returns the IReadOnlyCollection<IWebElement> that matches the specified parameters. </returns>
        public IReadOnlyCollection<IWebElement> GetElementsOnPage(string elementName, string pageName, string elementType = "", string pageType = "page")
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElements(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Gets element of page, which is currently stored in ScenarioContext.
        /// May cause some problems like InvalidCastException in PageElementHelper class.
        /// Should be used only after any NavigateToPageByName(), GetElementOnPage() or GetElementsOnPage() has been already called,
        /// otherwise won't work as expected.
        /// Not suitable for Navigation bar, should be modified or additional method should be created.
        /// </summary>
        /// <param name="elementName">lement Name (see ElementName attribute values in pages classes). </param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes). </param>
        /// <returns>Returns the IWebElement that matches the specified parameters. </returns>
        public IWebElement GetElementOnCurrentPage(string elementName, string elementType)
        {
            return PageElementHelper.GetElement(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Gets collection of similar elements of page, which is currently stored in ScenarioContext.
        /// May cause some problems like InvalidCastException in PageElementHelper class.
        /// Should be used only after any of NavigateToPageByName(), GetElementOnPage() or GetElementsOnPage() has been called,
        /// otherwise won't work as expected.
        /// Not suitable for Navigation bar, should be modified or additional method should be created.
        /// </summary>
        /// <param name="elementName">lement Name (see ElementName attribute values in pages classes). </param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes). </param>
        /// <returns>Returns the IReadOnlyCollection<IWebElement> that matches the specified parameters. </returns>
        public IReadOnlyCollection<IWebElement> GetElementsOnCurrentPage(string elementName, string elementType)
        {
            return PageElementHelper.GetElements(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Get specific product by exact name.
        /// </summary>
        /// <param name="productName">Name of product to find. </param>
        /// <returns>IWebElement of exact product. </returns>
        public IWebElement GetProductByName(string productName)
        {
            this.PerfromJSWait();

            return this.GetElementsOnPage("Products", "Products")
                .FirstOrDefault(x => x.FindElement(By.ClassName("card-title")).Text == productName)
                .FindElement(By.XPath("./.."));
        }

        /// <summary>
        /// Gets collection of IWebElement representation of Products according to the search 
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public IReadOnlyCollection<IWebElement> GetProductsBySearchRequest(string searchType, string searchRequest)
        {
            this.PerfromJSWait();

            switch (searchType)
            {
                case "name":
                    {
                        return this.GetElementsOnPage("Products", "Products")
                            .Where(x => 
                            x.FindElement(By.ClassName("card-title"))
                            .Text.Contains(searchRequest))
                            .ToList();
                    }
                case "brand name":
                    {
                        return this.GetElementsOnPage("Products", "Products")
                            .Where(x => 
                            x.FindElement(By.Id("productBrand"))
                            .Text.Contains(searchRequest))
                            .ToList();
                    }
                default:
                    {
                        throw new NotSupportedException($"Specified search type: '{searchType}' is not supported.");
                    }
            }
        }

        /// <summary>
        /// Navigates to the specified page by it's name.
        /// Will work only with appropriate URL path settings
        /// i.e. for Products page URL should be like %base_url%/products, etc. 
        /// </summary>
        /// <param name="pageName">Name of the page to navigate. </param>
        public async Task NavigateToPageByName(string pageName)
        {
            string urlPath = $"{pageName.Trim().Replace(" ", "").ToLowerInvariant()}";
            string finalUrl = String.Concat(this.ApplicationUrl, "/", urlPath);

            this.Driver.Navigate().GoToUrl(finalUrl);
            SetPage(pageName + " " + "page");

            this.PerfromJSWait();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Calls the Constant Message by appropriate Attribute name.
        /// </summary>
        /// <param name="messageName">Name of the constant message. </param>
        /// <returns>The string representation of a constant message. </returns>
        public string GetConstantMessageByName(string messageName)
        {
            return ConstantsHelper.GetConstantMessage(messageName);
        }

        /// <summary>
        /// Calls the Defaulter text value by appropriate Attribute name.
        /// It only works with defaulters which contains some text for comparison.
        /// </summary>
        /// <param name="pageName">Name of the page which should contain the target defaulter. </param>
        /// <returns>The string representation of a defaulter text. </returns>
        public string GetPageDefaulterValueByName(string pageName)
        {
            return ConstantsHelper.GetConstantPageDefaulterValue(pageName);
        }

        /// <summary>
        /// Sets the page to ScenarioContext to share it between steps and classes 
        /// and to make the page objects accessible without calling the page.
        /// </summary>
        /// <param name="pageName">Name of the page to set. </param>
        public void SetPage(string pageName)
        {
            this.ScenarioContext.Set(PageNavigationHelper.GetPage(this.ScenarioContext, this.FeatureContext, pageName));
            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void PerfromJSWait(int timeout = 10)
        {
            new WebDriverWait(this.Driver, TimeSpan.FromSeconds(timeout))
                .Until((Driver)
                => ((IJavaScriptExecutor)this.Driver)
                .ExecuteScript("return document.readyState")
                .ToString()
                .Equals("complete"));
        }

        /// <summary>
        /// This method supposed to compare two objects of the same type and ensure that all properties are equal.
        /// Should be used for models comparison only, not suitable for List<T> comparison.
        /// </summary>
        /// <typeparam name="T">Type of object to compare. Representation of the data model. </typeparam>
        /// <param name="expected">Object with expected properties. </param>
        /// <param name="actual">Object with actual properties. </param>
        /// <param name="ignore">Ignore list of properties which should not be compared. </param>
        /// <returns>Boolean result, which indicates either properties are equal or not. </returns>
        public static bool EnsureObjectPropertiesAreEqual<T>(T expected, T actual, params string[] ignore) where T : class
        {
            if (expected != null && actual != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(expected, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(actual, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return expected == actual;
        }
    }
}
