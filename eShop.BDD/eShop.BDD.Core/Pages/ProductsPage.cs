using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Defaulter")]
        public IWebElement Defaulter 
            => this.Wrapper.WaitForElement(By.TagName("h3"));

        [ElementName(@"Products")]
        public IReadOnlyCollection<IWebElement> Products 
            => this.Wrapper.WaitForElements(By.ClassName("card-body"));

        [ElementName(@"Search input")]
        public IWebElement SearchInput
            => this.Wrapper.WaitForElement(By.Id("filter"));

        [ElementName(@"Search button")]
        public IWebElement SearchButton
            => this.Wrapper.WaitForElement(By.CssSelector("form button"));

        [ElementName(@"No products found, try search again notification")]
        public IWebElement NoProductsNotification
            => this.Wrapper.WaitForElement(By.Id("noProductsMessage"));

        [ElementName(@"Reset Search button")]
        public IWebElement ResetSearchButton
            => this.Wrapper.WaitForElement(By.Id("resetSearchButton"));
    }
}
