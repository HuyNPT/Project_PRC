using AudioStreaming.WebAdminAS.Models.PlaylistDetail;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public class PlaylistDetailServiceWebAdmin : IPlaylistDetailServiceWebAdmin
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public PlaylistDetailServiceWebAdmin(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }
        public async Task<bool> DeletePlaylistDetail(string PlaylistId, string MediaId, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await client.DeleteAsync(_config["api-v"] + "/PlaylistDetails/" + PlaylistId+"/"+MediaId);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PlaylistDetailcs>> GetDetailsPlaylistByID(string PlaylistId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/PlaylistDetails/"+PlaylistId);
            var body = await response.Content.ReadAsStringAsync();
            var listPlaylistDetails = JsonConvert.DeserializeObject<List<PlaylistDetailcs>>(body);
            return listPlaylistDetails;
        }

        public async Task<bool> PostPlaylistDetail(string PlaylistId, string MediaId, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            //var httpContent = new StringContent(null, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_config["api-v"] + "/PlaylistDetails/" + PlaylistId + "/" + MediaId,null);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
