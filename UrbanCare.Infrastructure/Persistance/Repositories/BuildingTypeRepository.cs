using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class BuildingTypeRepository : IBuildingTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public BuildingTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BuildingType>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BuildingTypes.ToListAsync(cancellationToken);
        }

        public async Task<BuildingType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.BuildingTypes.FindAsync(id, cancellationToken);
        }
    }
}
