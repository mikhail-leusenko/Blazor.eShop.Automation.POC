using eShop.CoreBusiness.Models;
using eShop.DataStore.HardCoded;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.BDD.Core.Data
{
    public class TestProductRepository : IProductRepository
    {
        private readonly string ImageLink = "https://st3.depositphotos.com/16030310/18317/v/450/depositphotos_183173562-stock-illustration-letters-logo-initial-logo-identity.jpg";

        private List<Product> Products;

        public TestProductRepository()
        {
            this.Products = new List<Product>()
            {
                new Product { ProductId = 001, Brand = "QA & QC", Name = "Test product 1", Price = 9.99, Description = "Some test description just to populate all fields.", ImageLink = this.ImageLink },
                new Product { ProductId = 002, Brand = "QA & QC", Name = "Test 2", Price = 19.99, Description = "Product for search by partial name verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 003, Brand = "Unique brand", Name = "Test product 3", Price = 29.99, Description = "Product for brand search verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 004, Brand = "Testing", Name = "Test product 4", Price = 39.99, Description = "Product for brand search verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 005, Brand = "QA & QC", Name = "product 5", Price = 49.99, Description = "Product for search by partial name verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 006, Brand = "QA", Name = "Test product 6", Price = 59.99, Description = "Product for partial brand search verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 007, Brand = "QC", Name = "Test product 7", Price = 69.99, Description = "Product for partial brand search verification.", ImageLink = this.ImageLink },
                new Product { ProductId = 008, Brand = "QA & QC", Name = "Unique name product", Price = 79.99, Description = "Product for single result search verification.", ImageLink = this.ImageLink },
            };

        }

        public Product GetProduct(int id)
        {
            return this.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<Product> GetProducts(string filter = null)
        {
            if (string.IsNullOrWhiteSpace(filter)) return this.Products;

            return this.Products.Where(x => x.Name.ToLower().Contains(filter.ToLower()) 
            || x.Brand.ToLower().Contains(filter.ToLower()));
        }
    }
}
