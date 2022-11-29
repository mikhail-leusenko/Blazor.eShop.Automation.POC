using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class ShoppingCartPage : BasePage
    {
        public ShoppingCartPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Defaulter")]
        public IWebElement Defaulter
            => this.Wrapper.WaitForElement(By.TagName("h3"));

        [ElementName(@"Back to Products button")]
        public IWebElement BackToProductsButton
            => this.Wrapper.WaitForElement(By.Id("backToProductsBtn"));

        [ElementName(@"Back to Products link")]
        public IWebElement BackToProductsLink
            => this.Wrapper.WaitForElement(By.Id("backToProductsLink"));

        [ElementName(@"Products in cart")]
        public IReadOnlyCollection<IWebElement> ProductsInCart
            => this.Wrapper.WaitForElements(By.CssSelector("ul li.row"));

        [ElementName(@"Place your order button")]
        public IWebElement PlaceYourOrderButton
            => this.Wrapper.WaitForElement(By.Id("placeYourOrderBtn"));

        [ElementName(@"Total Price")]
        public IWebElement TotalPrice
            => this.Wrapper.WaitForElement(By.Id("totalPrice"));

        [ElementName(@"Total Items Count")]
        public IWebElement TotalItemsCount
            => this.Wrapper.WaitForElement(By.Id("itemsCount"));

    }
}
