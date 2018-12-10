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
            Console.WriteLine("CreateProduct: "
            + JsonConvert.SerializeObject(newProduct));
        }
        public void UpdateProduct(Product changedProduct)
        {
            Console.WriteLine("UpdateProduct : "
            + JsonConvert.SerializeObject(changedProduct));
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
