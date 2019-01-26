using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public interface ISupplierRepository
    {
        Supplier Get(long id);
        IEnumerable<Supplier> GetAll();
        void Create(Supplier newSupplier);
        void Update(Supplier updatedSupplier);
        void Delete(long id);
    }
}
