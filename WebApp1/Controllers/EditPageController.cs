using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace Ads.Controllers
{
    public class EditPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            ViewBag.AdvertisementId = id;

            return View(advertisement);
        }

        public IActionResult Details(int id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == id);

            return View(advertisement); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Advertisement model)
        {
            if (ModelState.IsValid)
            {
                var existingAdvertisement = _context.Advertisements.FirstOrDefault(a => a.Id == model.Id);
                if (existingAdvertisement == null)
                {
                    return NotFound();
                }

                // Обновите данные объявления
                existingAdvertisement.Title = model.Title;
                existingAdvertisement.Description = model.Description;
                existingAdvertisement.Price = model.Price;
                existingAdvertisement.Address = model.Address;
                existingAdvertisement.PhoneNumber = model.PhoneNumber;
                existingAdvertisement.ImageUrl = model.ImageUrl;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View("Create", model);
        }
    }
}
