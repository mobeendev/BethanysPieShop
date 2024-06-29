using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {
            var allPies = _bethanysPieShopDbContext.Categories;

            return View(allPies);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}

