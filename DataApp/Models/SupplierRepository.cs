using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly EFDatabaseContext context;

        public SupplierRepository(EFDatabaseContext ctx)
        {
            context = ctx;
        }

        public Supplier Get(long id)
        {
            return context.Suppliers.Find(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return context.Suppliers;
        }

        public void Create(Supplier newSupplier)
        {
            context.Add(newSupplier);
            context.SaveChanges();
        }

        public void Update(Supplier updatedSupplier)
        {
            context.Update(updatedSupplier);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            context.Remove(Get(id));
            context.SaveChanges();
        }

    }
}
