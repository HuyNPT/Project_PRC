using ASS_PRC.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASS_PRC.Api.Models.Request;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ASS_PRC.Api.Helpers;
using System.Security.Claims;

namespace ASS_PRC.Api.Controllers
{
    [Authorize]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
       
        [AllowAnonymous]
        [HttpGet]
        [Route("{PlaylistID}")]
        public async Task<IActionResult> GetPlayListDetails(Guid PlaylistID, [FromQuery] GetPlaylistDetailsRequest request)
        {
            try
            {
                var result = await _mediaService.GetPlayListDetails(PlaylistID, request.SortType, request.IsPaging, request.PageNumber, request.PageLimitItem, request.Type);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
            
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMedia([FromQuery] GetMediaRequest request) 
        {
            try{
                var rs = await _mediaService.GetMedia(request.SortType, request.IsPaging, request.PageNumber, request.PageLimitItem, request.Type, request.SearchKey, request.OrderBy, request.CategoryName);
                return Ok(JsonConvert.SerializeObject(rs));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            

        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]

        public async Task<IActionResult> PostNediaAdmin([FromBody] PostMediaRequest request)
        {
            Guid UserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Guid PlaylistID=new Guid();
            if (request.PlaylistId != null)
            {
                PlaylistID = new Guid(request.PlaylistId);
            }
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs = await _mediaService.PostMedia(request.MediaName, request.Url, request.ImgUrl, request.Author, request.Singer, request.Type, request.Category,UserID, PlaylistID, request.FileName, OwnerCode);
            if(rs){
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("{MediaID}")]
        public async Task<IActionResult> DeleteMediaAdmin(Guid MediaID)
        {
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs = await _mediaService.DeleteMediaWebAdmin(MediaID, OwnerCode);            
            if (!rs)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpGet("Count")]
        public async Task<IActionResult> GetMediaCount()
        {

            return Ok(_mediaService.GetMediaCount());
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> PutMedia([FromBody]PutMediaRequestcs rq) 
        {
            Guid UserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs = await _mediaService.PutMedia(rq.MediaID, rq.MediaName, rq.Url, rq.ImgUrl, rq.Author, rq.Singer, rq.Type, rq.Category, UserID,rq.FileName, OwnerCode);
            if (rs == "Unauthorized")
            {
                return Unauthorized();
            }
            else if(rs == "Fail")
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpGet("GetMediaById/{MediaID}")]
        public async Task<IActionResult> GetMediaById(Guid MediaID)
        {
            var rs = await _mediaService.GetMediaByID(MediaID);
            return Ok(JsonConvert.SerializeObject(rs));
        }

    }
    

}