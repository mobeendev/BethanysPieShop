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

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryName,Description, CategoryId")] Category updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            Console.WriteLine(category);

            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.CategoryName = updatedCategory.CategoryName;
                category.Description = updatedCategory.Description;
                _context.Update(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(updatedCategory);
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
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

