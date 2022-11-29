using eShop.BDD.Core.Steps;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace eShop.BDD.UI.Steps
{
    public class NavigationSteps : BaseSteps
    {
        public NavigationSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Given(@"user is on the ""(.*)"" page")]
        public async Task GivenUserIsOnThePage(string pageName)
        {
            this.NavigateToPageByName(pageName);

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [When(@"user clicks on ""(.*)"" navigation (link|logo)")]
        public void WhenUserClicksOnLink(string elementName, string elementType)
        {
            this.GetElementOnPage(elementName,
                elementType: elementType,
                pageName: "Navigation",
                pageType: "Bar")
                .Click();

            switch (elementName)
            {
                case "Cart":
                    this.SetPage("Shopping Cart page");
                    break;
                case "eShop":
                    this.SetPage("Products page");
                    break;
                default:
                    throw new NotSupportedException($"It seems that navigation is not supported on {elementName} click."+
                        $"{Environment.NewLine} Try to use {nameof(SharedSteps.WhenUserClicksOnButton)} instead.");
            }

        }

        [When(@"user navigates to the View Product page of ""(.*)"" product")]
        public void WhenUserNavigatesToThePageOfProduct(string productName)
        {
            this.PerfromJSWait();

            this.GetProductByName(productName)
                .FindElement(By.ClassName("card-title"))
                .Click();
        }
    }
}
