using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace Ads.Controllers
{
    public class AuthorizationPageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorizationPageController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }
        // GET: AuthorizationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuthorizationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorizationController/Registration
        public ActionResult Registration()
        {
            return View("Registration");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        // GET: AuthorizationController/Create
        [HttpPost]
        public IActionResult Log(User model)
        {
            // Используйте Dependency Injection, чтобы получить доступ к контексту базы данных
            var user = _context.FindUserByNameAndPassword(model.Name, model.Password);

            if (user != null)
            {
                // Пользователь найден, выполните необходимые действия
                // Например, установите какой-то флаг, сохраните его идентификацию в сеансе и перенаправьте его на другую страницу.
                var cookieOptions = new CookieOptions
                {
                    // Установите срок действия куки, если нужно
                    Expires = DateTime.Now.AddDays(1) // Например, куки будут действовать 1 день
                };

                // Создайте и установите куки с именем пользователя
                Response.Cookies.Append("UserName", user.Name, cookieOptions);

                Console.WriteLine("Успешный вход");
                return RedirectToAction("Index", "Home");

            }
            else
            {
                // Пользователь не найден, выполните другие действия
                // Например, отобразите сообщение об ошибке или перенаправьте его на страницу снова для входа.
                //ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");
                //return View("Registration");
                Console.WriteLine("Безуспешная попытка входа");
                return View("Registration");

            };
        }

        public IActionResult RedirectToUser()
        {
            var advertisements = _context.Advertisements.ToList(); // Преобразуйте DbSet в List
            return View("User", advertisements);
        }

        // GET: AuthorizationController/Create
        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Обработка случая, если файл не был выбран
            }


            // Если ModelState недействителен, верните представление с ошибками
            return View("Registration");

        }

        // GET: AuthorizationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorizationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
