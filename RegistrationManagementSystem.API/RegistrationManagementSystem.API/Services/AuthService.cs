using RegistrationManagementSystem.API.DTOs;
using RegistrationManagementSystem.API.Helpers;
using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories;
using RegistrationManagementSystem.API.Repositories.Interfaces;
using RegistrationManagementSystem.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace RegistrationManagementSystem.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepository, JwtHelper jwtHelper)
        {
            _authRepository = authRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<User?> RegisterAsync(RegisterDTO dto)
        {
            if (await _authRepository.UsernameExistsAsync(dto.Username))
                return null;

            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            return await _authRepository.CreateUserAsync(user);
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var user = await _authRepository.GetUserByUsernameAsync(dto.Username);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;
            return _jwtHelper.GenerateToken(user);
        }
    }
}