using eShop.CoreBusiness.Models;
using eShop.DataStore.HardCoded;
using System.Collections.Generic;

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
