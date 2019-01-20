﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return context.Products
                .Include(p => p.Supplier).First(p => p.Id == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.Include(p => p.Supplier);
        }

        public void CreateProduct(Product newProduct)
        {
            newProduct.Id = 0;
            context.Products.Add(newProduct);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newProduct.Id}");
        }

        public void UpdateProduct(Product changedProduct, Product originalProduct = null)
        {
            if (originalProduct == null)
            {
                originalProduct = context.Products.Find(changedProduct.Id);
            }
            else
            {
                context.Products.Attach(originalProduct);
            }
            originalProduct.Name = changedProduct.Name;
            originalProduct.Category = changedProduct.Category;
            originalProduct.Price = changedProduct.Price;
            originalProduct.Supplier.Name = changedProduct.Supplier.Name;
            originalProduct.Supplier.City = changedProduct.Supplier.City;
            originalProduct.Supplier.State = changedProduct.Supplier.State;
            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            //Product p = context.Products.Find(id);
            //context.Products.Remove(p);
            Product p = this.GetProduct(id);
            context.Products.Remove(p);

            if(p.Supplier != null)
            {
                context.Remove<Supplier>(p.Supplier);
            }

            context.SaveChanges();
        }

        public IEnumerable<Product> GetFilteredProducts(
                string category = null, 
                decimal? price = null,
                bool includeRelated = true)
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

            if(includeRelated)
            {
                data = data.Include(p => p.Supplier);
            }

            return data;
        }
    }
}
