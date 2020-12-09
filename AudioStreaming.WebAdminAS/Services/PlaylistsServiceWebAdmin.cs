using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AudioStreaming.WebAdminAS.Models;
using AudioStreaming.WebAdminAS.Services.Requests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AudioStreaming.WebAdminAS.Services
{
    public class PlaylistsServiceWebAdmin : IPlaylistsServiceWebAdmin
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public PlaylistsServiceWebAdmin(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<bool> DeletePlaylist(string PlaylistID, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);                      
           
            var response = await client.DeleteAsync(_config["api-v"] + "/Playlists/"+PlaylistID);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PlaylistResponse>> getPlaylist(int Page,string jwt)
        {
           
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);       
            var response = await client.GetAsync(_config["api-v"] + "/Playlists/admin?SortType=1&IsPaging=true&PageNumber="+Page+"&PageLimitItem=10");
            var body = await response.Content.ReadAsStringAsync();
            var listPlaylist = JsonConvert.DeserializeObject<List<PlaylistResponse>>(body);
            return listPlaylist;
        }

        public async Task<PlaylistResponse> GetPlaylistById(string PlaylistId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);            
            var response = await client.GetAsync(_config["api-v"] + "/Playlists/GetByID/" + PlaylistId);
            var body = await response.Content.ReadAsStringAsync();
            var playlist = JsonConvert.DeserializeObject<PlaylistResponse>(body);
            return playlist;
        }

        public async Task<int> GetPlaylistCount(string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.GetAsync(_config["api-v"] + "/Playlists/Count");
            var body = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(body);            
            return data.result;
        }

        public async Task<bool> PostPlaylist(string PlaylistName, int DateFillter, string ImageUrl, List<Guid> Category, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            PostPlaylistRequest req = new PostPlaylistRequest()
            {
                playlistName = PlaylistName,
                dateFillter = DateFillter,
                imageUrl = ImageUrl,
                category = Category
            };
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_config["api-v"] + "/Playlists/admin", httpContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> PutPlayListWebAdmin(string PlaylistId, string Name, int DateFilter, string ImageUrl, List<Guid> Category, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            PutPlaylistRequest req = new PutPlaylistRequest()
            {
                playlistID =PlaylistId,
                playlistName = Name,
                dateFillter = DateFilter,
                imageUrl = ImageUrl,
                category = Category
            };
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(_config["api-v"] + "/Playlists", httpContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "Success";
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Unauthorized";
            }
            else
            {
                return "Fail";
            }
        }
    }
}
