using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class WallMaterialRepository : IWallMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public WallMaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WallMaterial>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.WallMaterials.ToListAsync(cancellationToken);
        }

        public async Task<WallMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.WallMaterials.FindAsync(id, cancellationToken);
        }
    }
}
