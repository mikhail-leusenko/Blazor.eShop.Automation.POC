using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.BDD.Core.Models
{
    public class ProductOrderConfirmationModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}