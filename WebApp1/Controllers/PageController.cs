using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gavno()
        {
            return View();
        }
    }
}
