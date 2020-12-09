using AudioStreaming.WebAdminAS.Services;
using AudioStreaming.WebAdminAS.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public class UsersService : IUsersServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public UsersService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<string> Authenticate(String token)
        {
            AuthenticateModelWebAdmin req = new AuthenticateModelWebAdmin()
            {
                IdToken = token
            };
            var json = JsonConvert.SerializeObject(req);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config["BaseUrl"]);
            var response = await client.PostAsync(_config["api-v"]+"/Account/authenticate-webadmin", httpContent);
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Unauthorized";
            }
            var jwt = await response.Content.ReadAsStringAsync();
            return jwt;
        }
    }
}