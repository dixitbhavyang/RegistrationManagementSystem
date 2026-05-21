using RegistrationManagementSystem.API.DTOs;
using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDTO dto);
        Task<string?> LoginAsync(LoginDTO dto);
    }
}
