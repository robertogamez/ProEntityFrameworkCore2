using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdvancedContext context;

        public HomeController(AdvancedContext ctx)
        {
            this.context = ctx;
        }

        public IActionResult Index()
        {
            return View(context.Employees.AsNoTracking());
        }

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN)
            ? new Employee() : context.Employees.Include(e => e.OtherIdentity)
                .AsNoTracking()    
                .First(e => e.SSN == SSN
                            && e.FirstName == firstName
                            && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            Employee existing = context.Employees
                .AsTracking()
                .First(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName
                        && e.FamilyName == employee.FamilyName);
            if (existing == null)
            {
                context.Add(employee);
            }
            else
            {
                existing.Salary = employee.Salary;
            }

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}