using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Pages
{
    public class PlaceOrderPage : BasePage
    {
        public PlaceOrderPage(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [ElementName(@"Defaulter")]
        public IWebElement Defaulter
            => this.Wrapper.WaitForElement(By.CssSelector("div .main h3"));

        [ElementName(@"Name input")]
        public IWebElement NameInput
            => this.Wrapper.WaitForElement(By.Id("name"));

        [ElementName(@"Address input")]
        public IWebElement AddressInput
            => this.Wrapper.WaitForElement(By.Id("address"));

        [ElementName(@"City input")]
        public IWebElement CityInput
            => this.Wrapper.WaitForElement(By.Id("city"));

        [ElementName(@"State/Province input")]
        public IWebElement StateProvinceInput
            => this.Wrapper.WaitForElement(By.Id("state"));

        [ElementName(@"Country input")]
        public IWebElement CountryInput
            => this.Wrapper.WaitForElement(By.Id("country"));

        [ElementName(@"Place Order button")]
        public IWebElement PlaceOrderButton
            => this.Wrapper.WaitForElement(By.Id("placeOrderBtn"));

        [ElementName(@"Validation Messages")]
        public IReadOnlyCollection<IWebElement> ValidationMessages
            => this.Wrapper.WaitForElements(By.ClassName("validation-message"));

        
    }
}
