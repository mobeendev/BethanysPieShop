using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly BethanysPieShopDbContext _context;

        public CategoryController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _context = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {
            var allPies = _context.Categories;

            return View(allPies);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail(int? id)
        {
            var category = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();

            if (id == null || category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (id != null || category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return NotFound();

        }
    }
}

