using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class QualificationCategoryRepository : IQualificationCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public QualificationCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<QualificationCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.QualificationCategories.ToListAsync(cancellationToken);
        }

        public async Task<QualificationCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.QualificationCategories.FindAsync(id, cancellationToken);
        }
    }
}
