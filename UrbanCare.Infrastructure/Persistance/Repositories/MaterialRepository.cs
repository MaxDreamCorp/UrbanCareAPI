using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Material material, CancellationToken cancellationToken = default)
        {
            await _context.Materials.AddAsync(material, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Material>> GetAllByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Materials
                .Include(m => m.Storage)
                .Where(m => m.Storage.ManagementCompanyId == companyId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Material?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Materials.FindAsync(id, cancellationToken);
        }

        public async Task RemoveAsync(Material material, CancellationToken cancellationToken = default)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Material material, CancellationToken cancellationToken = default)
        {
            var existingMaterial = await _context.Materials.FindAsync(material.Id, cancellationToken);
            if (existingMaterial == null)
                throw new Exception("Данного материала не существует");

            existingMaterial.Name = material.Name;
            existingMaterial.Unit = material.Unit;
            existingMaterial.Price = material.Price;
            existingMaterial.StorageId = material.StorageId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
