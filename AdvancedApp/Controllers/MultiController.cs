using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdvancedApp.Controllers
{
    public class MultiController : Controller
    {
        private readonly AdvancedContext advancedContext;
        private readonly ILogger<MultiController> logger;

        public MultiController(AdvancedContext advancedContext, ILogger<MultiController> logger)
        {
            this.advancedContext = advancedContext;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View("EditAll", advancedContext.Employees);
        }

        [HttpPost]
        public ActionResult UpdateAll(Employee[] employees)
        {
            foreach (Employee employee in employees)
            {
                try
                {
                    advancedContext.Update(employee);
                    advancedContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    advancedContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
            }
            //advancedContext.UpdateRange(employees);

            //advancedContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}