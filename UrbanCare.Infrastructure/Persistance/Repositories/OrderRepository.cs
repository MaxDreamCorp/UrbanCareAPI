using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o => o.OrderCategory)
                    .ThenInclude(oc => oc.Type)
                .Include(o => o.Priority)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<Order>> GetByResidentIdAsync(int residentId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o => o.OrderCategory)
                   .ThenInclude(oc => oc.Type)
               .Include(o => o.Priority)
               .Include(o => o.Status)
               .Where(o => o.ResidentId == residentId)
               .ToListAsync(cancellationToken);
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Orders.AnyAsync())
                return await _context.Orders.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task<List<OrderMaterial>?> GetOrderMaterialsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.OrderMaterials
                .Include(om => om.Material)
                .Where(om => om.OrderId == id)
                .ToListAsync(cancellationToken);
        }

        public async Task RemoveAsync(Order order, CancellationToken cancellationToken = default)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            var oldOrder = await _context.Orders.FindAsync(order.Id, cancellationToken);
            if (oldOrder == null)
                return false;

            oldOrder.Description = order.Description;
            oldOrder.OrderCategory = order.OrderCategory;
            oldOrder.Building = order.Building;
            oldOrder.Apartment = order.Apartment;
            oldOrder.Priority = order.Priority;
            oldOrder.ContactPhone = order.ContactPhone;
            oldOrder.ContactEmail = order.ContactEmail;
            oldOrder.ChangedAt = order.ChangedAt;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
