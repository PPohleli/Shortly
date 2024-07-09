using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Data;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        private IUrlsService _urlService;
        public UrlController(IUrlsService urlService) 
        {
            this._urlService = urlService;
        }

        public IActionResult Index()
        {
            var allUrls = _urlService.GetUrls().Select(url => new GetUrlVM()
            {
                Id = url.Id,
                OriginalLink = url.OriginalLink,
                ShortLink = url.ShortLink,
                NrOfClicks = url.NrOfClicks,
                userId = url.userId,

                User = url.User != null ? new GetUserVM()
                {
                    Id = url.User.Id,
                    FullName = url.User.FullName
                } : null

            }).ToList();

            
            return View(allUrls);
        }

        public IActionResult Create()
        {
            

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            _urlService.Delete(id);

            TempData["Message"] = $"Your URL was successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
