using eShop.BDD.Core.Steps;
using eShop.CoreBusiness.Models;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace eShop.BDD.UI.Steps
{
    public class ViewProductSteps : BaseSteps
    {
        public ViewProductSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Then(@"user can see the next Product data")]
        public void ThenUserCanSeeTheNextProductData(Table table)
        {
            var expectedProduct = GetExpectedProduct(table);

            var actualProduct = GetActualProduct();

            Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
            Assert.AreEqual(expectedProduct.Brand, actualProduct.Brand);
            Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
            Assert.AreEqual(expectedProduct.Price, actualProduct.Price);

        }

        private Product GetExpectedProduct(Table table)
        {
            var expectedProduct = new Product();

            foreach (var row in table.Rows)
            {
                expectedProduct.Name = row["Product Name"];
                expectedProduct.Brand = row["Product Brand"];
                expectedProduct.Description = row["Product Description"];
                expectedProduct.Price = Double.Parse(row["Product Price"]);
            }

            return expectedProduct;
        }

        private Product GetActualProduct()
        {
            return new Product
            {
                Name = this.GetElementOnPage("Product Name", "View Product").Text,
                Brand = this.GetElementOnPage("Product Brand", "View Product").Text,
                Description = this.GetElementOnPage("Product Description", "View Product").Text,
                Price = Double.Parse(this.GetElementOnPage("Product Price ", "View Product").Text.Replace("Price: $", "").Trim())
            };
        }
    }
}
