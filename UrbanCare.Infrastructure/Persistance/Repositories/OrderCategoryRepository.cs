using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class OrderCategoryRepository : IOrderCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrderCategories
                .Include(oc => oc.Type)
                .ToListAsync();
        }

        public async Task<OrderCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.OrderCategories.FindAsync(id, cancellationToken);
        }
    }
}
