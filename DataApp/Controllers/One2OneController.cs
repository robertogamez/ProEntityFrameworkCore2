using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataApp.Controllers
{
    public class One2OneController : Controller
    {
        private readonly EFDatabaseContext context;

        public One2OneController(EFDatabaseContext ctx) => context = ctx;

        public IActionResult Index()
        {
            return View(context.Set<ContactDetails>().Include(cd => cd.Supplier));
        }

        public IActionResult Create() => View("ContactEditor");

        public IActionResult Edit(long id)
        {
            ViewBag.Suppliers = context.Suppliers.Include(s => s.Contact);

            return View("ContactEditor", context.Set<ContactDetails>()
                .Include(cd => cd.Supplier).First(cd => cd.Id == id));
        }

        [HttpPost]
        public IActionResult Update(ContactDetails details,
            long? targetSupplierId, long[] spares)
        {
            if(details.Id == 0)
            {
                context.Add(details);
            } else
            {
                context.Update(details);
                if (targetSupplierId.HasValue)
                {
                    if (spares.Contains(targetSupplierId.Value))
                    {
                        details.SupplierId = targetSupplierId.Value;
                    }
                }
            }

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
