using Microsoft.AspNetCore.Mvc;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class CreatePageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreatePageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Advertisement model)
        {
            Console.WriteLine("Метод Create вызван.");
            if (ModelState.IsValid)
            {
                // Создаем новое объявление и сохраняем его в базе данных
                var newAdvertisement = new Advertisement
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    ImageUrl = model.ImageUrl
                };

                _context.Advertisements.Add(newAdvertisement);
                _context.SaveChanges();

                Console.WriteLine("был выполнен метод create");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Вывести ошибки валидации
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage); // Вывести ошибку в консоль
                    }
                }

                Console.WriteLine("был выполнен метод create");
                // Вернуть представление с моделью, чтобы пользователь мог видеть ошибки
                return View(model);
            }
        }
    }
}
