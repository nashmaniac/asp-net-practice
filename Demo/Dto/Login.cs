﻿using System;
namespace Demo.Dto
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public Boolean isSuccess { get; set; }
        public DateTime? expiresIn { get; set; }
    }
}
