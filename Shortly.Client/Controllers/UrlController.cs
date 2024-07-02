using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.Models;
using Shortly.Client.Data.ViewModels;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            //Fake Db data

            var allUrls = new List<GetUrlVM>()
            {
                new GetUrlVM()
                {
                    Id = 1,
                    OriginalLink = "https://original.com",
                    ShortLink = "orgnl",
                    NrOfClicks = 1,
                    userId = 1,
                },
                new GetUrlVM()
                {
                    Id = 2,
                    OriginalLink = "https://link1.com",
                    ShortLink = "lnk1",
                    NrOfClicks = 2,
                    userId = 2,
                },
                new GetUrlVM()
                {
                    Id = 3,
                    OriginalLink = "https://link2.com",
                    ShortLink = "lnk2",
                    NrOfClicks = 3,
                    userId = 3,
                }
            };

            return View(allUrls);
        }

        public IActionResult Create()
        {
            

            return RedirectToAction("Index");
        }
    }
}
