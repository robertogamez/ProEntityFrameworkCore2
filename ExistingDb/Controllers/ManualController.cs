using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;

namespace ExistingDb.Controllers
{
    public class ManualController : Controller
    {
        private readonly ManualContext context;

        public ManualController(ManualContext ctx) => context = ctx;

        public IActionResult Index()
        {
            ViewBag.Styles = context.ShoeStyles;
            ViewBag.Widths = context.ShoeWidths;

            return View(context.Shoes);
        }
    }
}
