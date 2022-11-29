using eShop.BDD.Core.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Steps
{
    [Binding]
    public class SharedSteps : BaseSteps
    {
        public SharedSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [When(@"user clicks on ""(.*)"" (button|link)")]
        public void WhenUserClicksOnButton(string elementName, string elementType)
        {
            PerfromJSWait();
            GetElementOnCurrentPage(elementName, elementType).Click();

            if (elementName == "Place your order")
            {
                this.SetPage("Place Order page");
            }
        }

        [When(@"user adds ""(.*)"" to the cart")]
        public void WhenUserAddsToTheCart(string productName)
        {
            this.PerfromJSWait();

            this.GetProductByName(productName)
                .FindElement(By.Id("addToCartButton"))
                .Click();
        }

        [When(@"user adds the next products to the cart")]
        public void WhenUserAddsTheNextProductsToTheCart(Table table)
        {
            foreach(var row in table.Rows)
            {
                for(int i = 0; i < int.Parse(row["Product Quantity"]); i++)
                {
                    WhenUserAddsToTheCart(row["Product Name"]);
                }
            }
        }


        [Then(@"the ""(.*)"" page is open")]
        public void ThenTheCartPageIsOpen(string pageName)
        {
            var pageDefaulter = this.GetElementOnPage("Defaulter", pageName);

            Assert.IsTrue(pageDefaulter.Displayed);

            Assert.AreEqual(this.GetPageDefaulterValueByName(pageName + " page"), pageDefaulter.Text);
        }


        // May be expanded by adding elementType variable
        [Then(@"the ""(.*)"" button displayed")]
        public void ThenTheButtonDisplayed(string elementName)
        {
            Assert.IsTrue(this.GetElementOnCurrentPage(elementName, "button").Displayed);
        }

        // May be expanded by adding elementType variable
        [Then(@"the ""(.*)"" button is not displayed")]
        public void ThenTheButtonIsNotDisplayed(string elementName)
        {
            try
            {
                this.GetElementOnCurrentPage(elementName, "button");
            }
            catch (TargetInvocationException exception) // TargetInvocationException --> WebDriverTimeoutException --> NoSuchElementException
            {
                Assert.AreEqual(typeof(NoSuchElementException), exception.InnerException.InnerException.GetType());
            }
        }

    }
}
