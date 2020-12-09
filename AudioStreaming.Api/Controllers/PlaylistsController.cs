using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AudioStreaming.Api.Helpers;
using AudioStreaming.Api.Models.Request;
using AudioStreaming.Api.Models.RequestAdmin;
using AudioStreaming.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AudioStreaming.Api.Controllers
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

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult GetPlaylists([FromQuery]GetPlaylistRequest request)
        {
            var result = _playlistService.GetPlayList(request.IsSort, request.IsDescending, request.IsPaging, request.PageNumber ,request.PageLimitItem,request.OrderBy , request.SearchKey);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("admin")]
        public async Task<IActionResult> GetPlaylistsAdmin([FromQuery] GetPlaylistAdminRequest request)
        {
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var result =await _playlistService.GetPlayListWebAdmin(request.SortType, request.IsPaging, request.PageNumber, request.PageLimitItem, request.SearchKey,OwnerCode);
            return Ok(JsonConvert.SerializeObject(result));
        }
        [Authorize(Roles = Role.User+ ","+ Role.Admin)]       
        [HttpGet("users")]
        public ActionResult GetUserFavotitePlaylists()
        {
            Guid userId =new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if(userId == null)
            {
                return StatusCode(401, "User is not valid");
            }
            var result = _playlistService.GetUserFavoritePlayList(userId);
            return Ok(JsonConvert.SerializeObject(result));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("{searchKey}")]
        public ActionResult GetPlayList([FromQuery]GetPlaylistRequest request)
        {
            var result = _playlistService.GetPlayList(request.IsSort, request.IsDescending, request.IsPaging, request.PageNumber, request.PageLimitItem, request.OrderBy, request.SearchKey);
            return Ok(JsonConvert.SerializeObject(result));
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut("admin/{playlistId}")]
        public async System.Threading.Tasks.Task<ActionResult> PutPlaylistAsync(Guid playlistId,[FromBody]Services.DTO.Playlist playlist)
        {
            Guid ownerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            if (ownerCode == null)
            {
                return Unauthorized("Your role not permission");
            }
            var result =await _playlistService.PutPlaylist(playlistId, playlist, ownerCode);
            if (result.Equals("Your role not permission")) return Unauthorized(result);
            if (result.Equals("Playlist not exist")) return NotFound(result);
            return Ok(result);
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

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> PutPlaylist([FromBody] PutPlaylistRequest rq)
        {
            Guid UserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Guid OwnerCode = new Guid(User.FindFirst("OwnerCode")?.Value);
            var rs = await _playlistService.PutPlayListWebAdmin(rq.PlaylistID, rq.PlaylistName, rq.DateFillter, rq.ImageUrl, rq.Category, UserID, OwnerCode);
            if (rs == "Unauthorized")
            {
                return Unauthorized();
            }
            else if (rs == "Fail")
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}