using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class EmployeeStatusRepository : IEmployeeStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.EmployeeStatuses.ToListAsync(cancellationToken);
        }

        public async Task<EmployeeStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.EmployeeStatuses.FindAsync(id, cancellationToken);
        }
    }
}
