using eShop.BDD.Core.Pages.Base;
using eShop.BDD.Core.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace eShop.BDD.UI.Steps
{
    public class SearchSteps : BaseSteps
    {
        public SearchSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [When(@"user searches ""(.*)""")]
        public async Task WhenUserSearches(string searchRequest)
        {
            await Task.Delay(TimeSpan.FromSeconds(1)); // timeout to avoid Search field cleanup on .SendKeys() execution

            this.PerfromJSWait();

            this.GetElementOnPage("Search", "Products", elementType: "input").SendKeys(searchRequest);

            this.PerfromJSWait();

            (new Actions(this.Driver))
                .MoveToElement(
                this.GetElementOnPage(
                    "Search", "Products", elementType: "button"))
                .Click()
                .Perform();

            this.PerfromJSWait();
            await Task.Delay(TimeSpan.FromSeconds(1)); // timeout to avoid Search field cleanup on .SendKeys() execution
        }


        [Then(@"the single product with (name|brand name) ""(.*)"" is displayed")]
        public void ThenTheTheProductWithNameIsDisplayed(string searchType, string searchRequest)
        {
            this.PerfromJSWait();

            var displayedProducts = GetProductsBySearchRequest(searchType, searchRequest);

            Assert.IsTrue(displayedProducts.Any());
            Assert.IsTrue(displayedProducts.Count == 1);
        }
        
        [Then(@"each displayed product contains ""(.*)"" in its (name|brand name)")]
        public void ThenEachDisplayedProductContainsInItsName(string searchRequest, string searchType)
        {
            this.PerfromJSWait();

            var displayedProducts = GetProductsBySearchRequest(searchType, searchRequest);
            
            Assert.IsTrue(displayedProducts.Any());
            
            switch(searchType)
            {
                case "name":
                    VerifyProductNamesAreMatchingSearch(displayedProducts, searchRequest);
                    break;
                case "brand name":
                    VerifyProductBrandNamesAreMatchingSearch(displayedProducts, searchRequest);
                    break;
                default:
                    throw new NotSupportedException($"The specified search type '{searchType}' is not supported.");
            }
        }

        [Then(@"no products are displayed")]
        public void ThenNoProductsAreDisplayed()
        {
            this.PerfromJSWait();

            //IReadOnlyCollection<IWebElement> elements = new List<IWebElement>();
            //try
            //{
            //    elements.Concat(this.GetElementsOnPage("Products", "Products"));
            //}
            //catch(NoSuchElementException)
            //{
            //    CollectionAssert.IsEmpty(elements);
            //}

            CollectionAssert.IsEmpty(this.GetElementsOnPage("Products", "Products"));
        }

        [Then(@"""(.*)"" notification message is displayed")]
        public void ThenNotificationMessageIsDisplayed(string message)
        {
            Assert.AreEqual(message, this.GetElementOnCurrentPage(message, "notification").Text);
        }

        private void VerifyProductNamesAreMatchingSearch(IReadOnlyCollection<IWebElement> products, string searchRequest)
        {
            var productNames = products.Select(x => x.FindElement(By.ClassName("card-title")).Text).ToList();
            foreach(var name in productNames)
            {
                Assert.IsTrue(name.Contains(searchRequest), $"Product '{name}' name does not contain specified value: '{searchRequest}'.");
            }
        }

        private void VerifyProductBrandNamesAreMatchingSearch(IReadOnlyCollection<IWebElement> products, string searchRequest)
        {
            var productBrandNames = products.Select(x => x.FindElement(By.Id("productBrand")).Text).ToList();
            foreach (var brandName in productBrandNames)
            {
                Assert.IsTrue(brandName.Contains(searchRequest), $"Product brand name '{brandName}' does not contain specified value: '{searchRequest}'.");
            }
        }

    }
}
