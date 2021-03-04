using System;
using System.Threading.Tasks;
using Demo.Dto;

namespace Demo.Interfaces
{
    public interface IUserService
    {
        Task<RegistrationResponse> CreateUserAsync(RegistrationViewModel model);
        Task<LoginResponse> LoginUserAsync(LoginViewModel model);
    }
}
