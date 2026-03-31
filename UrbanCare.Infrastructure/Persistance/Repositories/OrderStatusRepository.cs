using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrderStatuses.ToListAsync(cancellationToken);
        }

        public async Task<OrderStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.OrderStatuses.FindAsync(id, cancellationToken);
        }
    }
}
