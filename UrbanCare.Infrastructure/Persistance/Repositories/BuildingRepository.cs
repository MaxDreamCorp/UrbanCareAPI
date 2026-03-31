using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext _context;

        public BuildingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Building building, CancellationToken cancellationToken = default)
        {
            await _context.Buildings.AddAsync(building, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<Building?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Buildings
                .Include(b => b.BuildingType)
                .Include(b => b.FloorMaterial)
                .Include(b => b.WallMaterial)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Building>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            var regions = await _context.Regions
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.BuildingType)
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.WallMaterial)
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.FloorMaterial)
                .Where(r => r.ManagementCompanyId == companyId).ToListAsync(cancellationToken);

            if (regions == null) return null;

            var buildings = new List<Building>();

            foreach (var region in regions)
                buildings.AddRange(region.Buildings);

            return buildings;
        }

        public async Task<List<Building>?> GetByRegionIdAsync(int regionId, CancellationToken cancellationToken = default)
        {
            var region = await _context.Regions
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.BuildingType)
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.WallMaterial)
                .Include(r => r.Buildings)
                    .ThenInclude(b => b.FloorMaterial)
                .FirstOrDefaultAsync(r => r.Id == regionId);

            if (region == null) return null;

            return region.Buildings.ToList();
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Buildings.AnyAsync())
                return await _context.Buildings.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(Building building, CancellationToken cancellationToken = default)
        {
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Building building, CancellationToken cancellationToken = default)
        {
            var oldBuilding = await _context.Buildings.FindAsync(building.Id, cancellationToken);

            if (oldBuilding == null) return false;

            oldBuilding.Number = building.Number;
            oldBuilding.Address = building.Address;
            oldBuilding.BuildingType = building.BuildingType;
            oldBuilding.Region = building.Region;
            oldBuilding.YearBuilt = building.YearBuilt;
            oldBuilding.FloorCount = building.FloorCount;
            oldBuilding.WallMaterial = building.WallMaterial;
            oldBuilding.FloorMaterial = building.FloorMaterial;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
