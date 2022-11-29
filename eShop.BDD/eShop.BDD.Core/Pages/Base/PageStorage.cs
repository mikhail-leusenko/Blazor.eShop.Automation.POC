using eShop.BDD.Core.Attributes;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages.Base
{
    public class PageStorage
    {
        private readonly ScenarioContext ScenarioContext;
        private readonly FeatureContext FeatureContext;
        public PageStorage(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.ScenarioContext = scenarioContext;
            this.FeatureContext = featureContext;
        }

        [PageName(@"Navigation bar")]
        public BasePage NavigationBar
            => new NavigationBar(this.ScenarioContext, this.FeatureContext);

        [PageName(@"Shopping Cart page")]
        public BasePage ShoppingCartPage
            => new ShoppingCartPage(this.ScenarioContext, this.FeatureContext);

        [PageName(@"Products page")]
        public BasePage ProductsPage 
            => new ProductsPage(this.ScenarioContext, this.FeatureContext);

        [PageName(@"View Product page")]
        public BasePage ViewProductPage
            => new ViewProductPage(this.ScenarioContext, this.FeatureContext);

        [PageName(@"Place Order page")]
        public BasePage PlaceOrderPage
            => new PlaceOrderPage(this.ScenarioContext, this.FeatureContext);

        [PageName(@"Order Confirmation page")]
        public BasePage OrderConfirmationPage
            => new OrderConfirmationPage(this.ScenarioContext, this.FeatureContext);
    }
}
