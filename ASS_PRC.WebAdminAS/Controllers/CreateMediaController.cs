using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioStreaming.WebAdminAS.Models.Media;
using AudioStreaming.WebAdminAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioStreaming.WebAdminAS.Controllers
{
    [Authorize]
    public class CreateMediaController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMediaServiceWebAdmin _mediaServiceWebAdmin;
        public CreateMediaController(ICategoryService categoryService, IMediaServiceWebAdmin mediaServiceWebAdmin)
        {
            _categoryService = categoryService;
            _mediaServiceWebAdmin = mediaServiceWebAdmin;
        }
        public async Task<IActionResult> Index(string id,string name)
        {
            List<string> typeMedia = new List<string>();
            typeMedia.Add("Media");
            typeMedia.Add("Ad");
            var listCategory = await _categoryService.getCategory();
            CreateMedia x = new CreateMedia()
            {
                TypeMedia = typeMedia,
                Category=listCategory,
                PlaylistID = id,
                PlaylistName = name
            };
            return View(x);
        }
        public async Task<IActionResult> Create(CreateMedia model)
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
            var rs = await _mediaServiceWebAdmin.PostMedia(model.MediaName, model.Url, model.ImgURL, model.Author, model.Singer, type, cate, sessions, model.PlaylistID, model.FileName);
            if (rs)
            {
                if(model.PlaylistID != null)
                {
                    return RedirectToAction("ViewDetails", "Playlists", new { id = model.PlaylistID});
                }
                else
                {
                    return RedirectToAction("Index", "Media");
                }
                
            }
            else
            {
                return Content("Error");
            }
        }
    }
}
