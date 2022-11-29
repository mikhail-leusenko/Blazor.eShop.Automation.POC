using eShop.BDD.Core.WebDriver;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages.Base
{
    /// <summary>
    /// This is a parent class for pages representations and it allows to use shared stuff by all child classes.
    /// </summary>
    public class BasePage
    {
        public IWebDriver WebDriver { get; }
        public WebElementWrapper Wrapper { get; }

        public BasePage(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.WebDriver = featureContext.Get<IWebDriver>();
            this.Wrapper = new WebElementWrapper(featureContext);
        }
    }
}
