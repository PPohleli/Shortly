using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.ViewModels;
using Shortly.Data;
using Shortly.Data.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Shortly.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this._appDbContext = context;
        }

        public IActionResult Index()
        {
            //return View(new PostUrlVM());
            return View();
        }

        public IActionResult ShortenUrl(PostUrlVM postUrlVM)
        {
            //Validate model
            if (!ModelState.IsValid)
                return View("Index", postUrlVM);

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newUrl = new Url()
            {
                OriginalLink = postUrlVM.Url,
                ShortLink = GeneratShortUrl(6),
                NrOfClicks = 0,
                userId = loggedInUserId,
                DateCreated = DateTime.UtcNow
            };
            
            _appDbContext.Urls.Add(newUrl);
            _appDbContext.SaveChanges();
            TempData["Message"] = $"Your URL was shortened successfully to {newUrl.ShortLink}";
            return RedirectToAction("Index");
        }

        private string GeneratShortUrl(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
