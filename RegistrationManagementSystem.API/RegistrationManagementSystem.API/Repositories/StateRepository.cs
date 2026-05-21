using Microsoft.EntityFrameworkCore;
using RegistrationManagementSystem.API.Data;
using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories.Interfaces;

namespace RegistrationManagementSystem.API.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _context;

        public StateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<State>> GetAllAsync()
        {
            return await _context.States.ToListAsync();
        }
    }
}