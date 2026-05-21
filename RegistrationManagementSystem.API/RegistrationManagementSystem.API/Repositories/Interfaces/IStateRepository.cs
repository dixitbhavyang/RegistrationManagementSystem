using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllAsync();
    }
}
