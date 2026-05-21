using Microsoft.AspNetCore.Mvc;
using RegistrationManagementSystem.API.DTOs;
using RegistrationManagementSystem.API.Services;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRegistrationService _registrationService;

        public AuthController(IAuthService authService, IRegistrationService registrationService)
        {
            _authService = authService;
            _registrationService = registrationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO dto, [FromForm] List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.RegisterAsync(dto);
            if (user == null)
                return BadRequest(new { message = "Username already exists." });

            await _registrationService.CreateAsync(dto, user.Id, files);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password." });

            return Ok(new { token });
        }
    }
}