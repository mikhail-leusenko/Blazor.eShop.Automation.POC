using eShop.CoreBusiness.Models;
using eShop.DataStore.HardCoded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.BDD.Core.Data
{
    public class TestOrderRepository : OrderRepository
    {
        public void CleanUp()
        {
            base.orders = new Dictionary<int, Order>();
        }
    }
}
