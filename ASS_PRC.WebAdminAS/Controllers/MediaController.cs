using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioStreaming.WebAdminAS.Models.Media;
using AudioStreaming.WebAdminAS.Models.PlaylistDetail;
using AudioStreaming.WebAdminAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioStreaming.WebAdminAS.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private readonly IMediaServiceWebAdmin _mediaServiceWebAdmin;
        private readonly IPlaylistDetailServiceWebAdmin _playlistDetailServiceWebAdmin;
        private readonly ICategoryService _categoryService;
        public MediaController(IMediaServiceWebAdmin mediaServiceWebAdmin, IPlaylistDetailServiceWebAdmin playlistDetailServiceWebAdmin, ICategoryService categoryService)
        {            
            _mediaServiceWebAdmin = mediaServiceWebAdmin;
            _playlistDetailServiceWebAdmin = playlistDetailServiceWebAdmin;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(string page, string playlistId, string playlistName)
        {
            int pageCurrent = 1;
            try
            {
                pageCurrent = Int32.Parse(page);
            }
            catch (Exception e)
            {

            }
            var rs = await _mediaServiceWebAdmin.GetAllMedia(pageCurrent - 1);
            var count = rs.Counter;
            int totalPage = count / 10;
            if (count % 10 != 0)
                totalPage++;
            if (pageCurrent > totalPage)
            {
                pageCurrent--;
            }
            
            ViewBag.Page = pageCurrent;
            ViewBag.Count = totalPage;
            
            List<PlaylistDetailcs> playlistDetails = null;
            if(playlistId != null)
            {
                playlistDetails = await _playlistDetailServiceWebAdmin.GetDetailsPlaylistByID(playlistId);
                ViewBag.PlaylistId = playlistId;
                ViewBag.PlaylistName = playlistName;
                ViewBag.PlaylistDetails = playlistDetails;               
            }                  
            return View(rs);
        }

        public async Task<IActionResult> Add(string playlistId, string mediaId, string page, string playlistName)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var rs =await _playlistDetailServiceWebAdmin.PostPlaylistDetail(playlistId, mediaId, sessions);
            if (rs)
            {
                return RedirectToAction("Index", "Media", new { page = page , playlistId = playlistId, playlistName=playlistName });
            }
            else
            {
                return Content("Error");
            }
        }

        public async Task<IActionResult> Remove(string playlistId, string mediaId, string page, string playlistName)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var rs = await _playlistDetailServiceWebAdmin.DeletePlaylistDetail(playlistId, mediaId, sessions);
            if (rs)
            {
                return RedirectToAction("Index", "Media", new { page = page, playlistId = playlistId, playlistName = playlistName });
            }
            else
            {
                return Content("Error");
            }
        }


        public async Task<IActionResult> UpdateMediaPage(string id, string page, string playlistId, string playlistName)
        {
            var media = await _mediaServiceWebAdmin.GetMediaByID(id);
            var listCategory = await _categoryService.getCategory();

            ViewBag.Category = listCategory;
            ViewBag.Page = page;
            List<string> typeMedia = new List<string>();
            typeMedia.Add("Media");
            typeMedia.Add("Ad");
            ViewBag.TypeMedia = typeMedia;
            if (playlistId != null)
            {
                ViewBag.PlaylistId = playlistId;
                ViewBag.PlaylistName = playlistName;
            }
            return View(media);
        }
        public async Task<IActionResult> UpdateMedia(int page, MediaModel model, string playlistId, string playlistName)
        {
            var sessions = HttpContext.Session.GetString("Token");
            List<Guid> cate = new List<Guid>();
            Request.Form["category"].ToList().ForEach(e =>
            {
                cate.Add(new Guid(e.ToString()));
            });
            var typeMedia = Request.Form["type_media"];
            int type = 1;
            if (typeMedia == "Ad")
            {
                type = 2;
            }
            var rs = await _mediaServiceWebAdmin.PutMedia(model.Id.ToString(), model.MusicName, model.Url, model.ImageUrl, model.Author, model.Singer, type, cate, model.FileName, sessions);

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
                if (playlistId != null)
                {
                    return RedirectToAction("ViewDetails", "Playlists", new { id = playlistId });
                }
                else
                {
                    return RedirectToAction("Index", "Media", new { page = page });
                }
            }
        }
    }
}
