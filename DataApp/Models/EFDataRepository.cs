using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataApp.Models
{
    public class EFDataRepository : IDataRepository
    {
        private EFDatabaseContext context;

        public EFDataRepository(EFDatabaseContext ctx)
        {
            context = ctx;
        }

        public Product GetProduct(long id)
        {
            return context.Products.Find(id);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products;
        }

        public void CreateProduct(Product newProduct)
        {
            newProduct.Id = 0;
            context.Products.Add(newProduct);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newProduct.Id}");
        }

        public void UpdateProduct(Product changedProduct)
        {
            // Sin seguimiento de cambios
            //context.Products.Update(changedProduct);
            //context.SaveChanges();

            Product originalProduct = context.Products.Find(changedProduct.Id);
            originalProduct.Name = changedProduct.Name;
            originalProduct.Category = changedProduct.Category;
            originalProduct.Price = changedProduct.Price;
            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            Console.WriteLine("DeleteProduct: " + id);
        }

        public IEnumerable<Product> GetFilteredProducts(string category = null, decimal? price = null)
        {
            IQueryable<Product> data = context.Products;
            if(category != null)
            {
                data = data.Where(p => p.Category == category);
            }

            if(price != null)
            {
                data.Where(p => p.Price >= price);
            }

            return data;
        }
    }
}
