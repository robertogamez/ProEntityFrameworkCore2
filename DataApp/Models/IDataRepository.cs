using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public interface IDataRepository
    {
        Product GetProduct(long id);
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product newProduct);
        void UpdateProduct(Product changedProduct);
        void DeleteProduct(long id);
        IEnumerable<Product> GetFilteredProducts(string category = null, decimal? price = null);
    }
}
