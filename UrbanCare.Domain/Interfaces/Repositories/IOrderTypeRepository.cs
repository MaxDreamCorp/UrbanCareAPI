using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IOrderTypeRepository
    {
        Task<OrderType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<OrderType>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
