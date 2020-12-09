using AudioStreaming.WebAdminAS.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public CategoryService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }
        public async Task<List<Category>> getCategory()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.GetAsync(_config["api-v"] + "/Category");
            var body = await response.Content.ReadAsStringAsync();
            var listCategory = JsonConvert.DeserializeObject<List<Category>>(body);
            return listCategory;
        }
    }
}
