using Microsoft.EntityFrameworkCore;
using RegistrationManagementSystem.API.Data;
using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories.Interfaces;

namespace RegistrationManagementSystem.API.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Registration> CreateAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            return registration;
        }

        public async Task<(List<Registration> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? sortBy, string? filterByName)
        {
            var query = _context.Registrations
                .Include(r => r.User)
                .Include(r => r.State)
                .Include(r => r.City)
                .Include(r => r.Documents)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
                query = query.Where(r => r.Name.Contains(filterByName));

            query = sortBy switch
            {
                "name" => query.OrderBy(r => r.Name),
                "date" => query.OrderBy(r => r.CreatedAt),
                _ => query.OrderByDescending(r => r.CreatedAt)
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _context.Registrations
                .Include(r => r.User)
                .Include(r => r.State)
                .Include(r => r.City)
                .Include(r => r.Documents)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Registration> UpdateAsync(Registration registration)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
            return registration;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null) return false;
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}