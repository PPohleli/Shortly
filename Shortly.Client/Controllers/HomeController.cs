using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.ViewModels;
using System.Diagnostics;

namespace Shortly.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return View(new PostUrlVM());
            return View();
        }

        public IActionResult ShortenUrl(PostUrlVM postUrlVM)
        {
            //return View();
            return RedirectToAction("Index");
        }
    }
}
