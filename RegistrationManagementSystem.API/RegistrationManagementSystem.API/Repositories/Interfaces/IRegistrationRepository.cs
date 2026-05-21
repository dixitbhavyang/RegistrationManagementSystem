using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration> CreateAsync(Registration registration);
        Task<(List<Registration> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? sortBy, string? filterByName);
        Task<Registration?> GetByIdAsync(int id);
        Task<Registration> UpdateAsync(Registration registration);
        Task<bool> DeleteAsync(int id);
    }
}
