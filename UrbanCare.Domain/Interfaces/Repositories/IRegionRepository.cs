using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IRegionRepository
    {
        Task AddAsync(Region region, CancellationToken cancellationToken = default);
        Task RemoveAsync(Region region, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Region region, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Region?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Region>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Region>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
    }

}
