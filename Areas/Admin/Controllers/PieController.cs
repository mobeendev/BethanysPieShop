using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class PieController : Controller
    {
        private readonly BethanysPieShopDbContext _context;

        public PieController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _context = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {
            var allPies = _context.Pies.ToList();
            return View(allPies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name,Price,ShortDescription,IsPieOfTheWeek,InStock")] Pie pie)
        {
            string selectedValue = Request.Form["CategoryId"];
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            if (selectedValue == null)
            {
                ModelState.AddModelError("CategoryId", "Please select a category!");

                return View(pie);
            }

            if (ModelState.IsValid)
            {
                pie.CategoryId = int.Parse(selectedValue);
                _context.Add(pie);
                _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pie = await _context.Pies.FindAsync(id);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price,ShortDescription,IsPieOfTheWeek,InStock, PieId")] Pie pie)
        {

            if (pie == null || id != pie.PieId)
            {
                return NotFound();
            }
            var existingPie = await _context.Pies.FindAsync(id);
            string selectedValue = Request.Form["CategoryId"];

            if (ModelState.IsValid)
            {
                existingPie.Name = pie.Name;
                existingPie.Price = pie.Price;
                existingPie.ShortDescription = pie.ShortDescription;
                existingPie.IsPieOfTheWeek = pie.IsPieOfTheWeek;
                existingPie.InStock = pie.InStock;
                existingPie.CategoryId = int.Parse(selectedValue); ;
                _context.Update(existingPie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var pie = await _context.Pies.FindAsync(id);

            if (id != null || pie != null)
            {
                _context.Pies.Remove(pie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return NotFound();

        }
    }
}
