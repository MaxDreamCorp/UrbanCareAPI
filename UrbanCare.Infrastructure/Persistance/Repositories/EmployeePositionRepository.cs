using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class EmployeePositionRepository : IEmployeePositionRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeePositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeePosition>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.EmployeePositions.ToListAsync(cancellationToken);
        }

        public async Task<EmployeePosition?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.EmployeePositions.FindAsync(id, cancellationToken);
        }
    }
}
