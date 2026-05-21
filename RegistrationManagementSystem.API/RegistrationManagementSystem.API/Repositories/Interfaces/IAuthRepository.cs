using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<User> CreateUserAsync(User user);
    }
}
