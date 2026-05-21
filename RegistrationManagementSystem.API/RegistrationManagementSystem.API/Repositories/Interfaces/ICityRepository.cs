using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetByStateIdAsync(int stateId);
    }
}
