using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationManagementSystem.API.DTOs;
using RegistrationManagementSystem.API.Services;
using RegistrationManagementSystem.API.Services.Interfaces;
using System.Security.Claims;

namespace RegistrationManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RegisterDTO dto, [FromForm] List<IFormFile> files)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _registrationService.CreateAsync(dto, userId, files);
            if (!result)
                return BadRequest(new { message = "Registration failed." });
            return Ok(new { message = "Registration created successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? filterByName = null)
        {
            var (items, totalCount) = await _registrationService.GetAllAsync(page, pageSize, sortBy, filterByName);
            return Ok(new { items, totalCount, page, pageSize });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);
            if (registration == null)
                return NotFound(new { message = "Registration not found." });
            return Ok(registration);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromForm] RegisterDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _registrationService.UpdateAsync(id, dto, userId);
            if (!result)
                return NotFound(new { message = "Registration not found." });
            return Ok(new { message = "Registration updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _registrationService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Registration not found." });
            return Ok(new { message = "Registration deleted successfully." });
        }

        [HttpGet("document/{documentId}")]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            var result = await _registrationService.DownloadDocumentAsync(documentId);
            if (result == null)
                return NotFound(new { message = "Document not found." });
            return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
        }
    }
}