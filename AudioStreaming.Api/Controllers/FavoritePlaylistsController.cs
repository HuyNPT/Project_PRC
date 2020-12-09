using AudioStreaming.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AudioStreaming.Api.Models.Request;
using System;

namespace AudioStreaming.Api.Controllers
{
    [Authorize]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class FavoritePlaylistsController : ControllerBase
    {
        private readonly IFavoritePlaylistsService _favoritePlaylistService;
        public FavoritePlaylistsController(IFavoritePlaylistsService favoritePlaylistService)
        {
            _favoritePlaylistService = favoritePlaylistService;
        }
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult PostFavoritesPlaylist([FromBody] FavoritesPlaylistRequest request)
        {
            var rs = _favoritePlaylistService.PostFavoritePlaylists(request.PlaylistId, request.AccountId);
            if (rs)
            {
                return Ok(Content("Add successfully"));
            }
            else
            {
                return BadRequest();
            }

        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("{PlaylistId}/{AccountId}")]
        public IActionResult DeleteFavoritesPlaylist(Guid PlaylistId,Guid AccountId)
        {
            var rs = _favoritePlaylistService.DeleteFavoritePlaylists(PlaylistId, AccountId);
            if (rs)
            {
                return Ok(Content("Delete successfully"));
            }
            else
            {
                return BadRequest();
            }


        }     
    }
    
}
