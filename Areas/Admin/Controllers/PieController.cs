using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class PieController : Controller
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public PieController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {
            var allPies = _bethanysPieShopDbContext.Pies;

            foreach (var product in allPies)
            {
                Console.WriteLine($"Product Name: {product.Name}, CategoryId: {product.CategoryId}");

            }

            return View(allPies);
        }

        public IActionResult Create()
        {

            ViewData["CategoryId"] = new SelectList(_bethanysPieShopDbContext.Categories, "CategoryId", "CategoryName");

            return View();
        }
    }
}
