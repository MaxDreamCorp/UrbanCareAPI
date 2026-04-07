using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
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

        public async Task<List<Order>> GetByManagementCompanyIdAsync(int managementCompanyId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o => o.OrderCategory)
                   .ThenInclude(oc => oc.Type)
               .Include(o => o.Priority)
               .Include(o => o.Status)
               .Where(o => o.Resident.Apartment.Building.Region.ManagementCompanyId == managementCompanyId)
               .ToListAsync(cancellationToken);
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

        public async Task<List<Order>> GetByManagementCompanyIdAndStatusAsync(int managementCompanyId, OrderStatusEnum status, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o => o.OrderCategory)
                   .ThenInclude(oc => oc.Type)
               .Include(o => o.Priority)
               .Include(o => o.Status)
               .Where(o => o.Resident.Apartment.Building.Region.ManagementCompanyId == managementCompanyId
                   && o.Status.Id == (int)status)
               .ToListAsync(cancellationToken);
        }


        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Orders.AnyAsync())
                return await _context.Orders.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task<List<OrderExecutor>?> GetOrderExecutorsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.OrderExecutors.Where(oe => oe.OrderId == id).ToListAsync(cancellationToken);
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

        public async Task AppointDispatcherAsync(int orderId, Employee dispatcher, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            order.DispatcherId = dispatcher.Id;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AppointExecutorAsync(int orderId, Employee executor, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.Include(o => o.OrderExecutors).FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            if (order.OrderExecutors.Any(oe => oe.Id == executor.Id))
                throw new Exception("Этот исполнитель уже назначен на этот заказ");

            OrderExecutor orderExecutor = new OrderExecutor
            {
                Id = _context.OrderExecutors.Any() ? _context.OrderExecutors.Max(oe => oe.Id) + 1 : 1,
                OrderId = orderId,
                ExecutorId = executor.Id
            };
            order.OrderExecutors.Add(orderExecutor);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangeStatusAsync(int orderId, int statusId, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            var status = await _context.OrderStatuses.FindAsync(statusId, cancellationToken);
            if (status == null)
                throw new Exception("Такого статуса не существует");

            order.Status = status;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetAcceptedAtDateAsync(int orderId, DateTime acceptanceDate, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            if (order.AcceptedAt != null)
                throw new Exception("Дата принятия уже установлена");

            order.AcceptedAt = acceptanceDate;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetCompletedAtDateAsync(int orderId, DateTime completionDate, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            if (order.CompletedAt != null)
                throw new Exception("Дата завершения уже установлена");

            order.CompletedAt = completionDate;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Order>> GetByExecutorIdAsync(int executorId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(o => o.OrderCategory)
                   .ThenInclude(oc => oc.Type)
               .Include(o => o.Priority)
               .Include(o => o.Status)
               .Where(o => o.OrderExecutors.Any(oe => oe.ExecutorId == executorId))
               .ToListAsync(cancellationToken);
        }

        public async Task<bool> CheckIfExecutorIsAppointedToOrderAsync(int orderId, int executorId, CancellationToken cancellationToken = default)
        {
            return await _context.OrderExecutors.AnyAsync(oe => oe.OrderId == orderId && oe.ExecutorId == executorId, cancellationToken);
        }

        public async Task ConfirmCompletionByResidentAsync(int orderId, Resident resident, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            if (order.ResidentId != resident.Id)
                throw new Exception("Этот заказ не принадлежит этому жителю");

            if (order.StatusId != (int)OrderStatusEnum.MarkedAsCompletedByExecutor)
                throw new Exception("Заказ не находится в статусе 'Отмечен как выполненный исполнителем'");

            if (await _context.OrderExecutors.AnyAsync(oe => oe.OrderId == orderId && oe.WorkPayment != null && oe.WorkPayment > 0, cancellationToken)
                || await _context.OrderMaterials.AnyAsync(om => om.OrderId == orderId, cancellationToken))
                order.StatusId = (int)OrderStatusEnum.PendingPayment;
            else
            {
                order.StatusId = (int)OrderStatusEnum.Completed;
                order.CompletedAt = DateTime.Now;
            }

                await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
