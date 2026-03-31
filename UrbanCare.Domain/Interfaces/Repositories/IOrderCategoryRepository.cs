using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IOrderCategoryRepository
    {
        Task<OrderCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<OrderCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
