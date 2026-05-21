using Microsoft.AspNetCore.Mvc;
using RegistrationManagementSystem.API.Services;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _cityService.GetByStateIdAsync(stateId);
            return Ok(cities);
        }
    }
}