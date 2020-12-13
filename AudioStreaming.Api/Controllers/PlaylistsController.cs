using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ASS_PRC.Api.Helpers;
using ASS_PRC.Api.Models.Request;
using ASS_PRC.Api.Models.RequestAdmin;
using ASS_PRC.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASS_PRC.Api.Controllers
{
    [Authorize]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
      

        [Authorize(Roles = Role.Admin)]
        [HttpGet("admin")]
        public async Task<IActionResult> GetPlaylistsAdmin([FromQuery] GetPlaylistAdminRequest request)
        {
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var result =await _playlistService.GetPlayListWebAdmin(request.SortType, request.IsPaging, request.PageNumber, request.PageLimitItem, request.SearchKey,OwnerCode);
            return Ok(JsonConvert.SerializeObject(result));
        }
        
       
        [Authorize(Roles = Role.Admin)]
        [HttpPost("admin")]

        public async Task<IActionResult> PostPlaylistAdmin([FromBody] PostPlaylistRequestcs request)
        {
            Guid UserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);           
            Guid BrandID = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs=await _playlistService.PostPlayListWebAdmin(request.PlaylistName, request.DateFillter, request.ImageUrl, BrandID, request.Category, UserID);
            if (rs)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("{PlaylistID}")]
        public async Task<IActionResult> DeletePlaylistAdmin(Guid PlaylistID)
        {
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs=await _playlistService.DeletePlayListWebAdmin(PlaylistID, OwnerCode);
            if(rs == "Unauthorized")
            {
                return Unauthorized();
            }
            else if(rs == "Delete fail")
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpGet("Count")]
        public async Task<IActionResult> GetPlaylistCount()
        {
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            return Ok(_playlistService.GetPlaylistCount(OwnerCode));
        }
        [AllowAnonymous]
        [HttpGet("GetByID/{PlaylistID}")]       
        public async Task<IActionResult> GetPlaylistByID(Guid PlaylistID)
        {
            var rs=await _playlistService.GetPlaylistById(PlaylistID);
            if (rs == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(JsonConvert.SerializeObject(rs));
            }
        }       
    }
}