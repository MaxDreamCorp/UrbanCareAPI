using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Role>?> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
