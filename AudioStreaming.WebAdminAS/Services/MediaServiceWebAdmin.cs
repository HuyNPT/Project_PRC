using AudioStreaming.WebAdminAS.Models.Media;
using AudioStreaming.WebAdminAS.Services.Requests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public class MediaServiceWebAdmin : IMediaServiceWebAdmin
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public MediaServiceWebAdmin(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<bool> DeleteMedia(string MediaID, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await client.DeleteAsync(_config["api-v"] + "/Media/" + MediaID);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MediaDTO> GetAllMedia(int page)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/Media?SortType=1&IsPaging=true&PageNumber="+page+"&PageLimitItem=10&Type=1");
            var body = await response.Content.ReadAsStringAsync();
            var listMedia = JsonConvert.DeserializeObject<MediaDTO>(body);
            return listMedia;
        }

        public async Task<List<MediaModel>> GetMedia(string playlistID, int page)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/Media/"+ playlistID + "?SortType=1&IsPaging=false&PageNumber=" + page + "&PageLimitItem=5");
            var body = await response.Content.ReadAsStringAsync();
            var listMedia = JsonConvert.DeserializeObject<List<MediaModel>>(body);
            return listMedia;
        }

        public async Task<MediaModel> GetMediaByID(string MediaId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/Media/GetMediaById/" + MediaId);
            var body = await response.Content.ReadAsStringAsync();
            var media = JsonConvert.DeserializeObject<MediaModel>(body);
            return media;
        }

        public async Task<int> GetMediaCount()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/Media/Count");
            var body = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(body);
            return data.result;
        }

        public async Task<bool> PostMedia(string MediaName, string URl, string ImageUrl, string Author, string Singer, int Type, List<Guid> Category, string jwt, string PlaylistID,string FileName)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            PostMediaRequest req = new PostMediaRequest()
            {
                mediaName=MediaName,
                url=URl,
                imgUrl=ImageUrl,
                author=Author,
                singer=Singer,
                type=Type,
                playlistId = PlaylistID,
                category = Category,
                fileName = FileName
            };
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_config["api-v"] + "/Media", httpContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> PutMedia(string MediaId, string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, string FileName, string jwt)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            PutMediaRequest req = new PutMediaRequest()
            {
                mediaID = MediaId,
                mediaName = MediaName,
                url = Url,
                imgUrl = ImaUrl,
                author = Author,
                singer = Singer,
                type = Type,               
                category = Category,
                fileName = FileName
            };
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(_config["api-v"] + "/Media", httpContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "Success";
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
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
