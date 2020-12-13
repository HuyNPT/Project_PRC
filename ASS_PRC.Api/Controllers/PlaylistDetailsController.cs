using System;
using System.Threading.Tasks;
using ASS_PRC.Api.Helpers;
using ASS_PRC.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASS_PRC.Api.Controllers
{
    [Authorize]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class PlaylistDetailsController : ControllerBase
    {
        private readonly IPlaylistDetailsService _playlistDetails;
        public PlaylistDetailsController(IPlaylistDetailsService playlistDetails)
        {
            _playlistDetails = playlistDetails;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("{PlaylistID}")]
        public async Task<IActionResult> GetDetailsPlaylistByID(Guid PlaylistID)
        {
            var result = await _playlistDetails.GetDetailsPlaylistByID(PlaylistID);

            return Ok(JsonConvert.SerializeObject(result));
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("{PlaylistID}/{MediaID}")]
        public async Task<IActionResult> DeletePlaylistDetail(Guid PlaylistID, Guid MediaID)
        {
            var rs = await _playlistDetails.DeletePlaylistDetail(PlaylistID, MediaID);
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
        [HttpPost]
        [Route("{PlaylistID}/{MediaID}")]
        public async Task<IActionResult> PostPlaylistDetail(Guid PlaylistID, Guid MediaID)
        {
            var rs = await _playlistDetails.PostPlaylistDetail(PlaylistID, MediaID);
            if (rs)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
