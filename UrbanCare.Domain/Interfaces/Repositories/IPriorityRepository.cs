using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IPriorityRepository
    {
        Task<Priority?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Priority>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
