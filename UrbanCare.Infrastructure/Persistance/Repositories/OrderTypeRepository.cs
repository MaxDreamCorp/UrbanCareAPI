using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrderTypes.ToListAsync(cancellationToken);
        }

        public async Task<OrderType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.OrderTypes.FindAsync(id, cancellationToken);
        }
    }
}
