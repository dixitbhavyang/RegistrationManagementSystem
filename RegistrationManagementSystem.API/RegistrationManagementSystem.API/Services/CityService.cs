using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories;
using RegistrationManagementSystem.API.Repositories.Interfaces;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> GetByStateIdAsync(int stateId)
        {
            return await _cityRepository.GetByStateIdAsync(stateId);
        }
    }
}