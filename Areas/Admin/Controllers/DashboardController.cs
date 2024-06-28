using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Admin
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public DashboardController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
