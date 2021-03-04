using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Demo.Dto;
using Demo.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegistrationResponse> CreateUserAsync(RegistrationViewModel model)
        {
            if(model == null)
            {
                throw new NullReferenceException("registation model failed");
            }

            var identity = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identity, model.Password);
            if(result.Succeeded)
            {
                return new RegistrationResponse
                {
                    Message = "User created successfully",
                    isSuccessful = true
                };
            }

            return new RegistrationResponse
            {
                Message = "User did not create successfully",
                isSuccessful = false
            };
        }

        public async Task<LoginResponse> LoginUserAsync(LoginViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Login model failed");

            }

            var idenity = await _userManager.FindByEmailAsync(model.Email);
            if(idenity == null)
            {
                return new LoginResponse
                {
                    Message = "User not found",
                    isSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(idenity, model.Password);
            if(!result)
            {
                return new LoginResponse
                {
                    Message = "Password not matched",
                    isSuccess = false
                };
            }

            var claims = new[]
            {
                new Claim("Email", idenity.Email),
                new Claim(ClaimTypes.NameIdentifier, idenity.Id)
            };


            var token = new JwtSecurityToken(
                issuer: _configuration["Authorization:Issuer"],
                audience: _configuration["Authorization:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Authorization:Key"])
                        ), SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                expiresIn = token.ValidTo,
                Message = "Login successful",
                isSuccess = true
            };
        }
    }
}
