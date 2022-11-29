using eShop.BDD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.BDD.Core.Data
{
    public class Constants
    {
        public Constants() { }

        public partial class PageNames
        {
            public PageNames() { }

            [ConstantName(@"Shopping Cart page")]
            public string ShoppingCart 
                => new string("Shopping Cart");

            [ConstantName(@"Products page")]
            public string Products 
                => new string ("Search Products");

            [ConstantName(@"Place Order page")]
            public string PlaceOrder 
                => new string("Placing Order");

            [ConstantName(@"Order Confirmation page")]
            public string OrderConfirmation 
                => new string ("Your Order is Placed");
        }
        
        public partial class ValidationMessages
        {
            public ValidationMessages() { }

            [ConstantName(@"Name Field Validation message")] 
            public string NameValidationMessage 
                => new string("The CustomerName field is required.");

            [ConstantName(@"Address Field Validation message")]
            public string AddressValidationMessage 
                => new string("The CustomerAddress field is required.");

            [ConstantName(@"City Field Validation message")]
            public string CityValidationMessage 
                => new string("The CustomerCity field is required.");

            [ConstantName(@"State/Province Field Validation message")] 
            public string StateProvinceValidationMessage 
                => new string("The CustomerStateProvince field is required.");

            [ConstantName(@"Country Field Validation message")] 
            public string CountryValidationMessage 
                => new string("The CustomerCountry field is required.");
        }
    }
}
