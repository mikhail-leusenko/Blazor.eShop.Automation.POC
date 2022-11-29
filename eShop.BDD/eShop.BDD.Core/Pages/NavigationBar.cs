using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class NavigationBar : BasePage
    {
        public NavigationBar(ScenarioContext scenarioContext, FeatureContext featureContext)
            : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Cart link")]
        public IWebElement CartLink 
            => this.Wrapper.WaitForElement(By.CssSelector("a[href='cart']"));

        [ElementName(@"eShop logo")]
        public IWebElement EshopLogo
            => this.Wrapper.WaitForElement(By.ClassName("navbar-brand"));
    }
}
