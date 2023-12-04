using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index(string category, string searchQuery)
        {
            // Получите объявления только для выбранной категории
            var advertisements = _context.Advertisements
                .Where(a => string.IsNullOrEmpty(category) || a.Category == category)
                .ToList();

            // Если есть поисковый запрос, фильтруем объявления по запросу
            if (!string.IsNullOrEmpty(searchQuery))
            {
                advertisements = advertisements
                    .Where(a => a.Title.Contains(searchQuery) || a.Description.Contains(searchQuery))
                    .ToList();
            }

            ViewBag.SelectedCategory = category; // Передаем выбранную категорию в представление
            ViewBag.SearchQuery = searchQuery; // Передаем поисковый запрос в представление

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

        public IActionResult RedirectToAuthorize()
        {
            return RedirectToAction("Registration", "AuthorizationPage");
        }

        

        public IActionResult Unlogin()
        {
            var cookieOptions = new CookieOptions
            {
                // Установите срок действия куки, если нужно
                Expires = DateTime.Now.AddDays(1) // Например, куки будут действовать 1 день
            };
            Response.Cookies.Append("UserName", "", cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "AuthorizationPage");
        }
    }
}
