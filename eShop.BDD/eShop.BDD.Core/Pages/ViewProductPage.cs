using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class ViewProductPage : BasePage
    {
        public ViewProductPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Product Name")]
        public IWebElement ProductName
            => this.Wrapper.WaitForElement(By.CssSelector("h5.card-title"));

        [ElementName(@"Product Brand")]
        public IWebElement ProductBrand
            => this.Wrapper.WaitForElement(By.CssSelector("h6.card-subtitle"));

        [ElementName(@"Product Description")]
        public IWebElement ProductDescription
            => this.Wrapper.WaitForElement(By.CssSelector("p.card-text"));

        [ElementName(@"Product Price")]
        public IWebElement ProductPrice
            => this.Wrapper.WaitForElement(By.Id("productPrice"));

        [ElementName(@"Add to cart button")]
        public IWebElement AddToCartButton
            => this.Wrapper.WaitForElement(By.Id("addToCartButton"));

        [ElementName(@"Back To Products link")]
        public IWebElement BackToProductsLink
            => this.Wrapper.WaitForElement(By.Id("backToProducts"));
    }
}
