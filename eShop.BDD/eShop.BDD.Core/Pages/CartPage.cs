using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Back to Products button")]
        public IWebElement BackToProductsButton
            => this.Wrapper.WaitForElement(By.Id("backToProductsBtn"));

        [ElementName(@"Back to Products link")]
        public IWebElement BackToProductsLink
            => this.Wrapper.WaitForElement(By.Id("backToProductsLink"));

        [ElementName(@"Place your order button")]
        public IWebElement PlaceYourOrderButton
            => this.Wrapper.WaitForElement(By.Id("placeYourOrderBtn"));
    }
}
