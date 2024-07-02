using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.Models;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            var tempData = TempData["SuccessMessage"];
            var viewBag = ViewBag.Test1;
            var viewdate = ViewData["Test 2"];

            if (tempData != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            else
            {

            }
            return View();
        }

        public IActionResult Create()
        {
            var shortenedUrl = "'short";
            TempData["SuccessMessage"] = "Successful!";
            ViewBag.Test1 = "Test 1";
            ViewData["Test 2"] = "Test 2";

            return RedirectToAction("Index");
        }
    }
}
