using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        Task AddAsync(Material material, CancellationToken cancellationToken = default);
        Task RemoveAsync(Material material, CancellationToken cancellationToken = default);
        Task UpdateAsync(Material material, CancellationToken cancellationToken = default);
        Task<Material?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Material>> GetAllByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
    }
}
