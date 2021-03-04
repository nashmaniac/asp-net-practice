using System;
namespace Demo.Dto
{
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationResponse
    {
        public string Message { get; set; }
        public Boolean isSuccessful { get; set; }
    }
}
