using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Services.Interfaces
{
    public interface IStateService
    {
        Task<List<State>> GetAllAsync();
    }
}
