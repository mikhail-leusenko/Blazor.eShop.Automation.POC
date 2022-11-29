using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.WebDriver
{
    public class WebElementWrapper
    {
        public IWebDriver Driver { get; }
        public WebElementWrapper(FeatureContext featureContext)
        {
            this.Driver = featureContext.Get<IWebDriver>();
        }

        /// <summary>
        /// Explicit wait for element to be presented.
        /// </summary>
        /// <param name="by">Selector representation of the element to find.</param>
        /// <param name="waitInterval">Interwal for element being wait.</param>
        /// <returns>Requested IWebElement.</returns>
        public IWebElement WaitForElement(By by, int waitInterval = 10)
        {
            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitInterval));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(WaitForJsJqueryLoad(this.Driver));
                return wait.Until(w => w.FindElement(by));
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Element was not found after {waitInterval} seconds timeout: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Explicit wait for collection of elements to be presented.
        /// </summary>
        /// <param name="by">Selector representation of the elements to find.</param>
        /// <param name="waitInterval">Interwal for element being wait.</param>
        /// <returns>Requested collection of IWebElements.</returns>
        public IReadOnlyCollection<IWebElement> WaitForElements(By by, int waitInterval = 10)
        {

            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)this.Driver;

            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitInterval));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(WaitForJsJqueryLoad(this.Driver));
                return wait.Until(w => w.FindElements(by));
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Elements were not found after {waitInterval} seconds timeout: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Provides the delegate function which will indicate either page scripts are still running or not.
        /// </summary>
        /// <param name="driver">Instance of IWebDriver.</param>
        /// <returns>Bool, which indicates either scripst execution has been completed the execution or not. </returns>
        private Func<IWebDriver, bool> WaitForJsJqueryLoad(IWebDriver driver)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            return new Func<IWebDriver, bool>((driver) =>
                WaitForJs(javaScriptExecutor));
        }

        /// <summary>
        /// Provides the wait of JS scripts being executed.
        /// </summary>
        /// <param name="javaScriptExecutor">The instance of javaScriptExecutor to run appropriate verification script. </param>
        /// <returns>Bool, which indicates either scripst execution has been completed the execution or not. </returns>
        private bool WaitForJs(IJavaScriptExecutor javaScriptExecutor)
        {
            return javaScriptExecutor
                           .ExecuteScript("return document.readyState")
                            .ToString()
                            .Equals("complete");
        }

        /// <summary>
        /// Provides the wait of jQuerry scripts being executed.
        /// </summary>
        /// <param name="javaScriptExecutor">The instance of javaScriptExecutor to run appropriate verification script. </param>
        /// <returns>Bool, which indicates either scripst execution has been completed the execution or not. </returns>
        private bool WaitForJQuerry(IJavaScriptExecutor javaScriptExecutor)
        {
            return javaScriptExecutor
                            .ExecuteScript("return jQuery.active")
                            .Equals(0);
        }
    }
}
