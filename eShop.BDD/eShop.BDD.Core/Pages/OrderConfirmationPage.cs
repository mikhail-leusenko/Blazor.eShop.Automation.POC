using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class OrderConfirmationPage : BasePage
    {
        public OrderConfirmationPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Defaulter")]
        public IWebElement Defaulter
            => this.Wrapper.WaitForElement(By.TagName("h3"));

        [ElementName(@"Products table")]
        public IReadOnlyCollection<IWebElement> ProductsTable
            => this.Wrapper.WaitForElements(By.Id("product"));

        [ElementName(@"Customer Name")]
        public IWebElement CustomerName
            => this.Wrapper.WaitForElement(By.Id("customerName"));

        [ElementName(@"Customer Address")]
        public IWebElement CustomerAddress
            => this.Wrapper.WaitForElement(By.Id("customerAddress"));

        [ElementName(@"Customer City")]
        public IWebElement CustomerCity
            => this.Wrapper.WaitForElement(By.Id("customerCity"));

        [ElementName(@"Customer State/Province")]
        public IWebElement CustomerStateProvince
            => this.Wrapper.WaitForElement(By.Id("customerStateProvince"));

        [ElementName(@"Customer Country")]
        public IWebElement CustomerCountry
            => this.Wrapper.WaitForElement(By.Id("customerCountry"));
    }
}
