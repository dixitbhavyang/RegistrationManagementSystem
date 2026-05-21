using Microsoft.EntityFrameworkCore;
using RegistrationManagementSystem.API.Data;
using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories.Interfaces;

namespace RegistrationManagementSystem.API.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetByStateIdAsync(int stateId)
        {
            return await _context.Cities
                .Where(c => c.StateId == stateId)
                .ToListAsync();
        }
    }
}