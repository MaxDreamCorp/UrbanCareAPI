using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Region region, CancellationToken cancellationToken = default)
        {
            await _context.Regions.AddAsync(region, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Region>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Regions.Include(r => r.ManagementCompany).ToListAsync(cancellationToken);
        }

        public async Task<Region?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Regions.FindAsync(id, cancellationToken);
        }

        public async Task<List<Region>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            var managementCompany = await _context.ManagementCompanies
                .Include(mc => mc.Regions)
                .FirstOrDefaultAsync(mc => mc.Id == companyId, cancellationToken);

            if (managementCompany == null) return null;

            return managementCompany.Regions.ToList();
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Regions.AnyAsync())
                return await _context.Regions.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(Region region, CancellationToken cancellationToken = default)
        {
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Region region, CancellationToken cancellationToken = default)
        {
            var oldRegion = await GetByIdAsync(region.Id, cancellationToken);

            if (oldRegion == null) return false;

            oldRegion.Name = region.Name;
            oldRegion.CommonAddress = region.CommonAddress;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
