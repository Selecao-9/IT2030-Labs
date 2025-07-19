using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace WebApp.Controllers
{

    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private DataContext context;

        public FormController(DataContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index(long? id)
        {
            ViewBag.Categories = new SelectList(context.Categories,
                "CategoryId", "Name");
            return View("Form", await context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => id == null || p.ProductId == id)
            ?? new() { Name = string.Empty });
        }

        [HttpPost]
        public IActionResult SubmitForm(Product product)
        {
            TempData["product"] = JsonSerializer.Serialize(product);
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}

