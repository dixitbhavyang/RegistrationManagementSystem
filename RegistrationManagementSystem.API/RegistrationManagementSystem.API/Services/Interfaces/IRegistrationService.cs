using RegistrationManagementSystem.API.DTOs;

namespace RegistrationManagementSystem.API.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> CreateAsync(RegisterDTO dto, int userId, List<IFormFile> files);
        Task<(List<RegistrationListDTO> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? sortBy, string? filterByName);
        Task<RegistrationListDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, RegisterDTO dto, int userId);
        Task<bool> DeleteAsync(int id);
        Task<(byte[] FileBytes, string ContentType, string FileName)?> DownloadDocumentAsync(int documentId);
    }
}
