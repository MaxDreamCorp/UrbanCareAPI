using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default)
        {
            await _context.Apartments.AddAsync(apartment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Apartment>?> GetByBuildingIdAsync(int buildingId, CancellationToken cancellationToken = default)
        {
            return await _context.Apartments.Where(a => a.BuildingId == buildingId).ToListAsync(cancellationToken);
        }

        public async Task<Apartment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Apartments.FindAsync(id, cancellationToken);
        }

        public async Task<List<Apartment>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            var buildings = await _context.Buildings
                .Include(b => b.Apartments)
                .Include(b => b.Region)
                    .ThenInclude(r => r.ManagementCompany)
                .Include(b => b.BuildingType)
                .Include(b => b.FloorMaterial)
                .Include(b => b.WallMaterial)
                .Where(b => b.Region.ManagementCompanyId == companyId).ToListAsync(cancellationToken);

            if (buildings == null) return null;

            var apartments = new List<Apartment>();

            foreach (var building in buildings)
                apartments.AddRange(building.Apartments);

            return apartments;
        }

        public async Task<List<Apartment>?> GetByRegionIdAsync(int regionId, CancellationToken cancellationToken = default)
        {
            var buildings = await _context.Buildings
                .Include(b => b.Apartments)
                .Where(b => b.RegionId == regionId).ToListAsync(cancellationToken);

            if (buildings == null) return null;

            var apartments = new List<Apartment>();

            foreach (var building in buildings)
                apartments.AddRange(building.Apartments);

            return apartments;
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Apartments.AnyAsync())
                return await _context.Apartments.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task<bool> IsFreeByIdAsync(int apartmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Apartments
               .Where(a => a.Id == apartmentId)
               .AnyAsync(a => !a.Residents.Any(r => r.IsLiving == 1), cancellationToken);
        }

        public async Task RemoveAsync(Apartment apartment, CancellationToken cancellationToken = default)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default)
        {
            var oldApartment = await _context.Apartments.FindAsync(apartment.Id, cancellationToken);

            if (oldApartment == null) return false;

            oldApartment.Number = apartment.Number;
            oldApartment.Building = apartment.Building;
            oldApartment.Entrance = apartment.Entrance;
            oldApartment.Floor = apartment.Floor;
            oldApartment.RoomsCount = apartment.RoomsCount;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
