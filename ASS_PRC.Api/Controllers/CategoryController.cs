using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASS_PRC.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASS_PRC.Api.Controllers
{
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices) {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _categoryServices.getCategory();
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
