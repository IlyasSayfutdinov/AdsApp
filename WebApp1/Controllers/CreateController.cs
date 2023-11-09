using Microsoft.AspNetCore.Mvc;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class CreatePageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreatePageController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult Create(Advertisement model)
         {
             if (ModelState.IsValid)
             {
                 if (model.Id == 0)
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
                 }
                 else
                 {
                     // Редактируем существующее объявление
                     var existingAdvertisement = _context.Advertisements.FirstOrDefault(a => a.Id == model.Id);
                     if (existingAdvertisement == null)
                     {
                         return NotFound();
                     }

                     existingAdvertisement.Title = model.Title;
                     existingAdvertisement.Description = model.Description;
                     existingAdvertisement.Price = model.Price;
                     existingAdvertisement.Address = model.Address;
                     existingAdvertisement.PhoneNumber = model.PhoneNumber;
                     existingAdvertisement.ImageUrl = model.ImageUrl;
                 }

                 _context.SaveChanges();

                 return RedirectToAction("Index", "Home");
             }

             return View(model);
         }*/

        public IActionResult Edit(int id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            
            return View("Create", advertisement);
        }
        /*[HttpPost]
        public IActionResult Create(Advertisement model)
        {
            Console.WriteLine("Метод Create вызван.");
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    // Если Id больше нуля, значит, это редактирование существующего объявления
                    var existingAdvertisement = _context.Advertisements.FirstOrDefault(a => a.Id == model.Id);

                    if (existingAdvertisement == null)
                    {
                        return NotFound();
                    }

                    // Обновить данные объявления
                    existingAdvertisement.Title = model.Title;
                    existingAdvertisement.Description = model.Description;
                    existingAdvertisement.Price = model.Price;
                    existingAdvertisement.Address = model.Address;
                    existingAdvertisement.PhoneNumber = model.PhoneNumber;
                    existingAdvertisement.ImageUrl = model.ImageUrl;

                    _context.SaveChanges();

                    Console.WriteLine("был выполнен метод edit (редактирование)");

                    return RedirectToAction("Index", "Home");
                }
                else
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

                    Console.WriteLine("был выполнен метод create (создание)");

                    return RedirectToAction("Index", "Home");
                }
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

                Console.WriteLine("был выполнен метод create (создание) или edit (редактирование)");
                // Вернуть представление с моделью, чтобы пользователь мог видеть ошибки
                return RedirectToAction("Index", "Home");
            }
        }*/

        [HttpPost]
        public IActionResult Create(Advertisement model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Задайте папку для сохранения изображения
                    var uploadFolder = Path.Combine(_env.WebRootPath, "Images");

                    // Генерируйте уникальное имя файла, чтобы избежать конфликтов
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                    // Сформируйте путь к файлу
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Сохраните файл на сервере
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(fileStream);
                    }

                    // Сформируйте URL для изображения
                    var imageUrl = Path.Combine("Images", uniqueFileName);

                    // Присвойте URL полю ImageUrl в модели
                    model.ImageUrl = imageUrl;

                    // Теперь вы можете сохранить модель с информацией о файле в базе данных
                    _context.Advertisements.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Обработка случая, если файл не был выбран
                }
            }

            // Если ModelState недействителен, верните представление с ошибками
            return View(model);
        }


    }
}
