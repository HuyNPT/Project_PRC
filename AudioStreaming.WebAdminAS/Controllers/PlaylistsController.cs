using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioStreaming.WebAdminAS.Models;
using AudioStreaming.WebAdminAS.Models.PlaylistModel;
using AudioStreaming.WebAdminAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioStreaming.WebAdminAS.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistsServiceWebAdmin _playlistsServiceWebAdmin;
        private readonly IMediaServiceWebAdmin _mediaServiceWebAdmin;
        private readonly ICategoryService _categoryService;
        public PlaylistsController(IPlaylistsServiceWebAdmin playlistsServiceWebAdmin, IMediaServiceWebAdmin mediaServiceWebAdmin,ICategoryService categoryService)
        {
            _playlistsServiceWebAdmin = playlistsServiceWebAdmin;
            _mediaServiceWebAdmin = mediaServiceWebAdmin;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(string page)
        {
            var sessions = HttpContext.Session.GetString("Token");
            int pageCurrent = 1;
            try
            {
                pageCurrent = Int32.Parse(page);
            }catch (Exception e)
            {

            }
            var count = await _playlistsServiceWebAdmin.GetPlaylistCount(sessions);
            int totalPage = count / 10;
            if (count % 10 != 0)
                totalPage++;
            if (pageCurrent > totalPage)
            {
                pageCurrent--;
            }
            var rs =await _playlistsServiceWebAdmin.getPlaylist(pageCurrent-1,sessions);
            ViewBag.Page = pageCurrent;
            ViewBag.Count = totalPage;
            return View(rs);
        }
        public async Task<IActionResult> ViewDetails(string id)
        {
            var playlistSelected=await _playlistsServiceWebAdmin.GetPlaylistById(id);
            var listMedia = await _mediaServiceWebAdmin.GetMedia(id, 0);
            Playlist x = new Playlist()
            {
                PlaylistSelected = playlistSelected,
                ListMedia = listMedia
            };

            return View(x);
        }
        public async Task<IActionResult> DeletePlaylist(string id, int page)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var rs=await _playlistsServiceWebAdmin.DeletePlaylist(id, sessions);           
            if (rs)
            {
                return RedirectToAction("Index", "Playlists", new { page = page });
            }
            else
            {
                return Content("error");
            }
            
        }
        public async Task<IActionResult> DeleteMedia(string MediaId,string PlaylistID, string page)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var rs = await _mediaServiceWebAdmin.DeleteMedia(MediaId, sessions);
            if (rs)
            {
                if(page != null)
                {
                    return RedirectToAction("Index", "Media", new { page = page });
                }
                else
                {
                    return RedirectToAction("ViewDetails", "Playlists", new { id = PlaylistID });
                }
                
            }
            else
            {
                return Content("error or unauthorized");
            }
            
        }

        public async Task<IActionResult> UpdateplaylistPage(string id, string page)
        {
            var playlistSelected = await _playlistsServiceWebAdmin.GetPlaylistById(id);
            var listCategory = await _categoryService.getCategory();

            ViewBag.Category = listCategory;
            ViewBag.Page = page;
            return View(playlistSelected);
        }
        public async Task<IActionResult> UpdatePlaylist(int page, PlaylistResponse model)
        {
            var sessions = HttpContext.Session.GetString("Token");
            List<Guid> cate = new List<Guid>();
            Request.Form["category"].ToList().ForEach(e =>
            {
                cate.Add(new Guid(e.ToString()));
            });
            var rs = await _playlistsServiceWebAdmin.PutPlayListWebAdmin(model.Id.ToString(), model.PlaylistName, model.DateFillter, model.ImageUrl, cate, sessions);

            if (rs == "Unauthorized")
            {
                return Content("Unauthorized");
            }
            else if (rs == "Fail")
            {
                return Content("Error");
            }
            else
            {
                return RedirectToAction("Index", "Playlists", new { page = page });
            }
        }
    }
}
