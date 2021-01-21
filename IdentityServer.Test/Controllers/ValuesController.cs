using Microsoft.AspNetCore.Mvc;
using Nacos.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly INacosServerManager _serverManager;

        public ValuesController(INacosServerManager serverManager)
        {
            _serverManager = serverManager;
        }

        [HttpGet("test")]
        public async Task<string> Test()
        {
            var baseUrl = await _serverManager.GetServerAsync("testService");

            if(string.IsNullOrEmpty(baseUrl))
            {
                return "empty";
            }

            var url = $"{baseUrl}/api/values";
            using (HttpClient client=new HttpClient())
            {
                var result = await client.GetAsync(url);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
