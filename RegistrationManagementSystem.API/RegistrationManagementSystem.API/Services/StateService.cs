using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories;
using RegistrationManagementSystem.API.Repositories.Interfaces;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<List<State>> GetAllAsync()
        {
            return await _stateRepository.GetAllAsync();
        }
    }
}