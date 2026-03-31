using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IApartmentRepository
    {
        Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default);
        Task RemoveAsync(Apartment apartment, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Apartment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Apartment>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<Apartment>?> GetByRegionIdAsync(int regionId, CancellationToken cancellationToken = default);
        Task<List<Apartment>?> GetByBuildingIdAsync(int buildingId, CancellationToken cancellationToken = default);
        Task<bool> IsFreeByIdAsync(int apartmentId, CancellationToken cancellationToken = default);
    }
}
