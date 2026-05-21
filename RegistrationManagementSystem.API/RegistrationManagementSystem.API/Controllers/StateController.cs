using Microsoft.AspNetCore.Mvc;
using RegistrationManagementSystem.API.Services;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            var states = await _stateService.GetAllAsync();
            return Ok(states);
        }
    }
}