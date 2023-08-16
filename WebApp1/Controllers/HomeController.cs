using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var advertisements = _context.Advertisements.ToList();

            foreach (var ad in advertisements)
            {
                Console.WriteLine($"Title: {ad.Title}, Description: {ad.Description}");
            }


            return View(advertisements);
        }

        public IActionResult RedirectToCreate()
        {
            return RedirectToAction("Index", "CreatePage");
        }
    }
}
