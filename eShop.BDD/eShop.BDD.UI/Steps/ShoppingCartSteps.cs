using eShop.BDD.Core.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace eShop.BDD.UI.Steps
{
    [Binding]
    public class ShoppingCartSteps : BaseSteps
    {
        public ShoppingCartSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [When(@"user deletes ""(.*)"" product from the cart")]
        public void WhenUserDeletesFromTheCart(string productName)
        {
            DeleteProductFromCartByName(this.GetElementsOnCurrentPage("Products in cart", string.Empty), productName);
        }

        [When(@"user sets the ""(.*)"" quantity as ""(.*)""")]
        public async Task WhenUserSetsTheQuantityAs(string productName, int productQuantity)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            var productQty = this.GetElementsOnCurrentPage("Products in cart", string.Empty)
                .FirstOrDefault(x => x.FindElement(By.Id("itemName")).Text == productName)
                .FindElement(By.Id("itemQty"));
            productQty.Clear();
            productQty.SendKeys(productQuantity.ToString());
            productQty.SendKeys(Keys.Tab);
        }

        [Then(@"the ""(.*)"" price is (.*)")]
        public void ThenThePriceIs(string productName, string productPrice)
        {
            this.PerfromJSWait();
            var prodPrice = this.GetElementsOnCurrentPage("Products in cart", string.Empty)
                .FirstOrDefault(x => x.FindElement(By.Id("itemName")).Text == productName)
                .FindElement(By.Id("itemPrice"))
                .Text
                .Split("$")[1]
                .Trim();

            Assert.AreEqual(productPrice,
               prodPrice);
            //"^(-?)(0|([1-9][0-9]*))(\\.[0-9]+)?$"
        }

        [Then(@"total price value is (.*)")]
        public void ThenTotalPriceValueIs(double totalPrice)
        {
            this.PerfromJSWait();

            Assert.AreEqual(totalPrice,
                double.Parse(this.GetElementOnCurrentPage("Total", "Price")
                .Text
                .Split("$")[1]
                .Trim()));
        }


        [Then(@"the ""(.*)"" is displayed in cart")]
        public void ThenTheIsDisplayed(string productName)
        {
            CheckTheProductExistsInCart(this.GetElementsOnCurrentPage("Products in cart", string.Empty), productName);
        }

        [Then(@"the ""(.*)"" product is not displayed in cart")]
        public void ThenTheProductDoesNotExistInCart(string productName)
        {
            CheckTheProductDoesNotExistInCart(this.GetElementsOnCurrentPage("Products in cart", string.Empty), productName);
        }

        [Then(@"the next products are displayed in cart")]
        public async Task ThenTheNextProductsAreDisplayedInCart(Table table)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            var actualProducts = this.GetElementsOnCurrentPage("Products in cart", string.Empty);

            Assert.AreEqual(table.RowCount, actualProducts.Count);

            foreach (var row in table.Rows)
            {
                CheckTheProductExistsInCart(actualProducts, row["Product Name"]);
            }
        }

        [Then(@"the total items count is (.*)")]
        public void ThenTheTotalItemsCountIs(int totalItemsCount)
        {
            Assert.AreEqual(totalItemsCount,
                Int32.Parse(Regex.Match(this.GetElementOnCurrentPage("Total Items", "Count").Text, @"\d+").Value));
            //(this.GetElementOnCurrentPage("Total Items", "Count")
            //.Text
            //.Split("(")[1]
            //.Trim())); ;
        }

        [Then(@"total price is equal to the sum of all product prices multuiplied on it quantities and is (.*)")]
        public void ThenTotalPriceIsEqualToTheSumOfAllProductPricesMultuipliedOnItQuantitiesAndIs(double totalPrice)
        {
            double actualProductsPricesSum = 0;

            var actualProducts = this.GetElementsOnCurrentPage("Products in cart", string.Empty);

            foreach (var product in actualProducts)
            {
                //actualProductsPricesSum =+ double.Parse(product.FindElement(By.Id("itemPrice"))
                //    .Text
                //    .Split("$")[1]
                //    .Trim());

                var prodPr = double.Parse(product.FindElement(By.Id("itemPrice"))
                    .Text
                    .Split("$")[1]
                    .Trim(), CultureInfo.InvariantCulture);

                var prodQty = int.Parse(product.FindElement(By.Id("itemQty")).GetAttribute("value").Trim());

                actualProductsPricesSum += (prodPr * prodQty);
                    //double.Parse(product.FindElement(By.Id("itemPrice"))
                    //.Text
                    //.Split("$")[1]
                    //.Trim()) 
                    //* int.Parse(product.FindElement(By.Id("itemQty")).GetAttribute("value").Trim());
            }

            actualProductsPricesSum = Math.Round(actualProductsPricesSum, 2);

            Assert.AreEqual(totalPrice,
                actualProductsPricesSum,
                "The total products sum is differs from expected total price.");

            Assert.AreEqual(actualProductsPricesSum,
                double.Parse(this.GetElementOnCurrentPage("Total", "Price")
                .Text
                .Split("$")[1]
                .Trim(), CultureInfo.InvariantCulture),
                "The total products sum is differs from actual total price.");
        }


        /// <summary>
        /// This step was created in order to call it from step definitions with Table,
        /// which contains certain products, existance of which in cart need to be verified.
        /// </summary>
        /// <param name="productsInCart">Collection of products which were added to cart. </param>
        /// <param name="productName">Name of the product which is supposed to be in cart. </param>
        /// <param name="productQuantity">Quantity of the specified product. 0 by default if verification is not required. </param>
        /// <param name="productPrice">Price of the specified product. 0 by default if verification is not required. </param>
        private void CheckTheProductExistsInCart(IReadOnlyCollection<IWebElement> productsInCart, string productName, int productQuantity = 0, double productPrice = 0)
        {
            var expectedProduct = productsInCart.FirstOrDefault(x => x.FindElement(By.Id("itemName")).Text == productName);

            Assert.IsNotNull(expectedProduct);

            if (productQuantity > 0)
            {
                CheckTheQuantityOfProductInCart(expectedProduct, productQuantity);
            }

            if (productPrice != 0)
            {
                CheckThePriceOfProductInCart(expectedProduct, productPrice);
            }
        }

        private void CheckThePriceOfProductInCart(IWebElement productInCart, double productPrice)
        {
            Assert.AreEqual(productPrice,
                double.Parse(productInCart
                .FindElement(By.Id("itemPrice"))
                .Text
                .Split("$")[1]
                .Trim()));
        }

        private void CheckTheQuantityOfProductInCart(IWebElement productInCart, int productQuantity)
        {
            Assert.AreEqual(productQuantity,
               int.Parse(productInCart
               .FindElement(By.Id("itemQty"))
               .Text
               .Split("$")[1]
               .Trim()));
        }

        /// <summary>
        /// This step was created in order to call it from step definitions with Table,
        /// which contains certain products, non existance of which in cart need to be verified.
        /// </summary>
        /// <param name="productsInCart">Collection of products which were added to cart. </param>
        /// <param name="productName">Name of the product which is not supposed to be in cart. </param>
        private void CheckTheProductDoesNotExistInCart(IReadOnlyCollection<IWebElement> productsInCart, string productName)
        {
            try
            {
                productsInCart.FirstOrDefault(x => x.FindElement(By.Id("itemName")).Text == productName);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoSuchElementException), ex.GetType());
            }
        }

        /// <summary>
        /// This step was created in order to call it from step definitions with Table,
        /// which contains certain products, which are supposed to be deleted from cart.
        /// </summary>
        /// <param name="productsInCart">Collection of products which were added to cart. </param>
        /// <param name="productName">Name of the product which is supposed to be deleted from cart. </param>
        private void DeleteProductFromCartByName(IReadOnlyCollection<IWebElement> productsInCart, string productName)
        {
            productsInCart
                .FirstOrDefault(x => x.FindElement(By.Id("itemName")).Text == productName)
                .FindElement(By.ClassName("btn-delete"))
                .Click();
        }
    }
}
