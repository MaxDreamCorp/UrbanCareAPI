using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<OrderStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<OrderStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
