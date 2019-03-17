using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExistingDb.Controllers
{
    public class ManualController : Controller
    {
        private readonly ManualContext context;

        public ManualController(ManualContext ctx) => context = ctx;

        public IActionResult Index()
        {
            ViewBag.Styles = context.ShoeStyles.Include(s => s.Products);
            ViewBag.Widths = context.ShoeWidths.Include(s => s.Products);
            ViewBag.Categories = context.Categories
                .Include(c => c.Shoes).ThenInclude(j => j.Shoe);

            return View(context.Shoes.Include(s => s.Style)
            .Include(s => s.Width).Include(s => s.Categories)
            .ThenInclude(j => j.Category));
        }
    }
}
