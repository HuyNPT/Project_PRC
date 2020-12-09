using AudioStreaming.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AudioStreaming.Api.Models.Request;
using Newtonsoft.Json;
using System;
using AudioStreaming.Api.Helpers;

namespace AudioStreaming.Api.Controllers
{
    [Authorize]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class CurrentMediaController : ControllerBase
    {
        private readonly ICurrentMedianStoresService _currentMedia;
        public CurrentMediaController(ICurrentMedianStoresService currentMedia)
        {
            _currentMedia = currentMedia;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetCurrentMedia()
        {
            var result = _currentMedia.GetAllCurrentMediaInStores();
            return Ok(JsonConvert.SerializeObject(result));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("{StoreID}")]
        public ActionResult GetByStoreID(Guid StoreID)
        {
            var result = _currentMedia.GetByStoreID(StoreID);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [Authorize(Roles =Role.CoffeeStore)]
        [HttpPut]
        public ActionResult PutCurrentPlaylistInStore(PutCurrentMediaRequest putCurrentMediaRequest)
        {
            DateTime startTime =new  DateTime();
            DateTime endTime = new DateTime();
            try { 
             startTime = DateTime.Parse(putCurrentMediaRequest.TimeStart);
             endTime = DateTime.Parse(putCurrentMediaRequest.TimeEnd);
            }
            catch (Exception){
                return BadRequest("Date time is invalid format");
            }
            var result = _currentMedia
                .PutCurrentMediaInStores(putCurrentMediaRequest.StoreId, putCurrentMediaRequest.PlaylistId, putCurrentMediaRequest.MediaId, startTime, endTime);
            return Ok(result);
        }
    }
}
