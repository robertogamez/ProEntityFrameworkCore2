using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers
{
    public class DeleteController : Controller
    {
        private readonly AdvancedContext context;

        public DeleteController(AdvancedContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Employees.Where(e => e.SoftDeleted)
                .Include(e => e.OtherIdentity).IgnoreQueryFilters());
        }

        [HttpPost]
        public IActionResult Restore(Employee employee)
        {
            context.Employees.IgnoreQueryFilters()
                .First(e => e.SSN == employee.SSN
                        && e.FirstName == employee.FirstName
                        && e.FamilyName == employee.FamilyName).SoftDeleted = false;
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            context.Database.ExecuteSqlCommand("EXECUTE PurgeSoftDelete");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RestoreAll()
        {
            context.Database.ExecuteSqlCommand("EXECUTE RestoreSoftDelete");
            return RedirectToAction(nameof(Index));
        }
    }
}