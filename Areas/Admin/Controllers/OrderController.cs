using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly BethanysPieShopDbContext _context;

        public OrderController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _context = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();

            return View(orders);
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
