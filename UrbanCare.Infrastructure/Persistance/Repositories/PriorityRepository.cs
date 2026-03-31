using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly ApplicationDbContext _context;

        public PriorityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Priority>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Priorities.ToListAsync(cancellationToken);
        }

        public async Task<Priority?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Priorities.FindAsync(id, cancellationToken);
        }
    }
}
