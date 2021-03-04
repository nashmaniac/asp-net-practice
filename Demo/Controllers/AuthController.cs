using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dto;
using Demo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;


        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> CreateUserAsync([FromBody] RegistrationViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new RegistrationResponse
                {
                    isSuccessful = false,
                    Message = "Model state is not valid"
                });
            }

            var response = await _userService.CreateUserAsync(model);
            if(response.isSuccessful)
            {
                return Ok(response);    
            }

            return BadRequest(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginUserAsync([FromBody] LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Token = null,
                    expiresIn = null,
                    Message = "Login model is invalid",
                    isSuccess = false
                });
            }

            return await _userService.LoginUserAsync(model);
        }
    }
}
