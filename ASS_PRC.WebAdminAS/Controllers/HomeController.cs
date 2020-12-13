using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AudioStreaming.WebAdminAS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using AudioStreaming.WebAdminAS.Services;

namespace AudioStreaming.WebAdminAS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlaylistsServiceWebAdmin _playlistsServiceWebAdmin;

        public HomeController(ILogger<HomeController> logger, IPlaylistsServiceWebAdmin playlistsServiceWebAdmin)
        {
            _logger = logger;
            _playlistsServiceWebAdmin = playlistsServiceWebAdmin;
        }

        public async Task<IActionResult> Index()
        {
            var user = User.Identity.Name;
            var sessions = HttpContext.Session.GetString("Token");            
            var rs = await _playlistsServiceWebAdmin.getPlaylist(0, sessions);
            ViewBag.Chart = rs;           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Users");
        }
    }
}
