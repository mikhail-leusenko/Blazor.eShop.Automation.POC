using eShop.BDD.Core.Models;
using eShop.BDD.Core.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace eShop.BDD.UI.Steps
{
    public class PlaceOrderSteps : BaseSteps
    {
        private SharedSteps SharedSteps;

        public PlaceOrderSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
            this.SharedSteps = new SharedSteps(scenarioContext, featureContext);
        }

        [When(@"user clicks on ""(.*)"" button to submit the order")]
        public async Task WhenUserClicksOnButtonToSubmitTheOrder(string buttonName)
        {
            this.PerfromJSWait();

            this.SharedSteps.WhenUserClicksOnButton(buttonName, "button");
            this.SetPage("Order Confirmation page");

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [When(@"user fills the personal information to place order")]
        public async Task WhenUserFillsThePersonalInformationToPlaceOrder(Table table)
        {
            this.PerfromJSWait();

            await Task.Delay(TimeSpan.FromSeconds(1));

            foreach (var row in table.Rows)
            {
                this.GetElementOnCurrentPage("Name", "input").SendKeys(row["Name"]);

                this.GetElementOnCurrentPage("Address", "input").SendKeys(row["Address"]);

                this.GetElementOnCurrentPage("City", "input").SendKeys(row["City"]);

                this.GetElementOnCurrentPage("State/Province", "input").SendKeys(row["State/Province"]);

                this.GetElementOnCurrentPage("Country", "input").SendKeys(row["Country"]);
            }
        }

        [Then(@"page contains next product information")]
        public void ThenPageContainsNextProductInformation(Table table)
        {
            this.PerfromJSWait();

            var expectedProducts = GetExpectedProductsOnOrderConfirmation(table);

            var actualProductsElements = this.GetElementsOnPage("Products",
                elementType: "table",
                pageName: "Order Confirmation");

            var actualProducts = GetActualProductsOnOrderConfirmation(actualProductsElements);

            Assert.AreEqual(expectedProducts.Count, actualProducts.Count);

            foreach(var expectedProduct in expectedProducts)
            {
                var actualProduct = actualProducts.FirstOrDefault(x => x.Name == expectedProduct.Name);

                Assert.IsTrue(EnsureObjectPropertiesAreEqual<ProductOrderConfirmationModel>(expectedProduct, actualProduct));
            }
        }

        [Then(@"page contains the next customer information")]
        public void ThenPageContainsTheNextCustomerInformation(Table table)
        {
            this.PerfromJSWait();

            var actualCustomerInfo = GetActualCustomerInfo();

            var expectedCustomerInfo = GetExpectedCustomerInfo(table);

            Assert.IsTrue(EnsureObjectPropertiesAreEqual(expectedCustomerInfo, actualCustomerInfo));
        }


        [Then(@"the validation messages are displayed for the next fields")]
        public void ThenTheValidationMessagesAreDisplayedForTheNextFields(Table table)
        {
            this.PerfromJSWait();

            var actualValidationMessages =
                this.GetElementsOnCurrentPage("Validation", "Messages")
                .Select(x => x.Text)
                .ToList();

            Assert.AreEqual(actualValidationMessages.Count, table.RowCount,
                $"The actual error messages quantity: {actualValidationMessages.Count} differs from the explected one: {table.RowCount}.");

            foreach (var row in table.Rows)
            {
                CollectionAssert.Contains(actualValidationMessages, this.GetConstantMessageByName(row["Field Name"] + " Field Validation message"));
            }
        }

        private List<ProductOrderConfirmationModel> GetExpectedProductsOnOrderConfirmation(Table table)
        {
            var expectedProducts = new List<ProductOrderConfirmationModel>();

            foreach (var row in table.Rows)
            {
                expectedProducts.Add(
                    new ProductOrderConfirmationModel
                    {
                        Name = row["Product Name"],
                        Quantity = Int32.Parse(row["Quantity"]),
                        TotalPrice = Double.Parse(row["Price"])
                    });
            }

            return expectedProducts;
        }

        private List<ProductOrderConfirmationModel> GetActualProductsOnOrderConfirmation(IReadOnlyCollection<IWebElement> actualProductsElements)
        {
            var actualProducts = new List<ProductOrderConfirmationModel>();

            foreach (var product in actualProductsElements)
            {
                actualProducts.Add(
                    new ProductOrderConfirmationModel
                    {
                        Name = product.FindElement(By.Id("productName")).Text,
                        Quantity = Int32.Parse(product.FindElement(By.Id("productQty")).Text),
                        TotalPrice = Double.Parse(product.FindElement(By.Id("productPrice")).Text)
                    });
            }

            return actualProducts;
        }

        private CustomerInfoOrderConfirmationModel GetActualCustomerInfo()
        {
            return new CustomerInfoOrderConfirmationModel
            {
                Name = this.GetElementOnCurrentPage("Customer", "Name").Text.Split(":")[1].Trim(),
                Address = this.GetElementOnCurrentPage("Customer", "Address").Text.Split(":")[1].Trim(),
                City = this.GetElementOnCurrentPage("Customer", "City").Text.Split(":")[1].Trim(),
                StateProvince = this.GetElementOnCurrentPage("Customer", "State/Province").Text.Split(":")[1].Trim(),
                Country = this.GetElementOnCurrentPage("Customer", "Country").Text.Split(":")[1].Trim()
            };
        }

        private CustomerInfoOrderConfirmationModel GetExpectedCustomerInfo(Table table)
        {
            var expectedCustomerInfo = new CustomerInfoOrderConfirmationModel();

            foreach (var row in table.Rows)
            {
                if (table.RowCount > 1)
                {
                    throw new InvalidOperationException("The multiple rows are not supported by this method. Verify the table population.");
                }

                expectedCustomerInfo = new CustomerInfoOrderConfirmationModel
                {
                    Name = row["Name"],
                    Address = row["Address"],
                    City = row["City"],
                    StateProvince = row["State/Province"],
                    Country = row["Country"]
                };
            }

            return expectedCustomerInfo;
        }

        
    }
}
