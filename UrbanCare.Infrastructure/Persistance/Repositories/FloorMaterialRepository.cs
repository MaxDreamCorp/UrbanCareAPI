using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class FloorMaterialRepository : IFloorMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public FloorMaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FloorMaterial>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.FloorMaterials.ToListAsync(cancellationToken);
        }

        public async Task<FloorMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.FloorMaterials.FindAsync(id, cancellationToken);
        }
    }
}
