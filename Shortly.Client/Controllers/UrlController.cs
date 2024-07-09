using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Data;
using Shortly.Data.Models;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        private IUrlsService _urlService;
        private readonly IMapper _mapper;
        public UrlController(IUrlsService urlService, IMapper mapper) 
        {
            this._urlService = urlService;
            this._mapper = mapper;  
        }

        public IActionResult Index()
        {
            var allUrls = _urlService.GetUrls();
            var mappedAllUrls = _mapper.Map<List<Url>, List<GetUrlVM>>(allUrls);
            
            
            return View(mappedAllUrls);
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
