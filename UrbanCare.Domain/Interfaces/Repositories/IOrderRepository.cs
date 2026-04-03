using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task RemoveAsync(Order order, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Order>> GetByResidentIdAsync(int residentId, CancellationToken cancellationToken = default);
        Task<List<OrderMaterial>?> GetOrderMaterialsByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<OrderExecutor>?> GetOrderExecutorsByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Order>> GetByManagementCompanyIdAsync(int managementCompanyId, CancellationToken cancellationToken = default);
        Task<List<Order>> GetByManagementCompanyIdAndStatusAsync(int managementCompanyId, OrderStatusEnum status, CancellationToken cancellationToken = default);

    }
}
