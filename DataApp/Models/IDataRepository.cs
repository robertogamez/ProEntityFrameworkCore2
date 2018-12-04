using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public interface IDataRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
