using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Client.Helpers.Roles;
using Shortly.Data;
using Shortly.Data.Models;
using Shortly.Data.Services;
using System.Security.Claims;

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

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole(Role.Admin);



            var allUrls = await _urlService.GetUrlsAsync(loggedInUserId, isAdmin);
            var mappedAllUrls = _mapper.Map<List<Url>, List<GetUrlVM>>(allUrls);
            
            return View(mappedAllUrls);
        }

        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _urlService.DeleteAsync(id);

            TempData["Message"] = $"Your URL was successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
