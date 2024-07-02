using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.Models;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            //Fake Db data

            var allUrls = new List<Url>()
            {
                new Url()
                {
                    Id = 1,
                    OriginalLink = "https://original.com",
                    ShortLink = "orgnl",
                    NrOfClicks = 1,
                    userId = 1,
                },
                new Url()
                {
                    Id = 2,
                    OriginalLink = "https://link1.com",
                    ShortLink = "lnk1",
                    NrOfClicks = 2,
                    userId = 2,
                },
                new Url()
                {
                    Id = 3,
                    OriginalLink = "https://link2.com",
                    ShortLink = "orgnl",
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
