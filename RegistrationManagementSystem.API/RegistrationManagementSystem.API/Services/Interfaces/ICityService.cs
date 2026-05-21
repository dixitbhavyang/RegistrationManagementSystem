using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Services.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetByStateIdAsync(int stateId);
    }
}
