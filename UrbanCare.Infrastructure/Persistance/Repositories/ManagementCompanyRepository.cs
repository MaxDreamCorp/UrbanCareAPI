using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class ManagementCompanyRepository : IManagementCompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagementCompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ManagementCompany managementCompany, CancellationToken cancellationToken = default)
        {
            await _context.ManagementCompanies.AddAsync(managementCompany, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ManagementCompany>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ManagementCompanies.ToListAsync(cancellationToken);
        }

        public async Task<ManagementCompany?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ManagementCompanies.FindAsync(id, cancellationToken);
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.ManagementCompanies.AnyAsync())
                return await _context.ManagementCompanies.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(ManagementCompany managementCompany, CancellationToken cancellationToken = default)
        {
            _context.ManagementCompanies.Remove(managementCompany);
            await _context.SaveChangesAsync();
        }
    }
}
