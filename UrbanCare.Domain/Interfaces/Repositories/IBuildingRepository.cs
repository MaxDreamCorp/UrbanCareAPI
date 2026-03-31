using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IBuildingRepository
    {
        Task AddAsync(Building building, CancellationToken cancellationToken = default);
        Task RemoveAsync(Building building, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Building building, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Building?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Building>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<Building>?> GetByRegionIdAsync(int regionId, CancellationToken cancellationToken = default);
    }
}
