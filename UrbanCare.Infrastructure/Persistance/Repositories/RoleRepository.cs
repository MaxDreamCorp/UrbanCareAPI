using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }

        public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles.FindAsync(id, cancellationToken);
        }
    }
}
