using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dto;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        private JsonPlaceholderService _jsonService { get; set; }
        public JsonController(JsonPlaceholderService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("/users")]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _jsonService.GetUsersAsync();
        }
    }
}
