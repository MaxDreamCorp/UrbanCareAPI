using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IResidentRepository
    {
        Task AddAsync(Resident resident, CancellationToken cancellationToken = default);
        Task RemoveAsync(Resident resident, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Resident resident, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Resident?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Resident?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<Resident?> GetCurrentByApartmentIdAsync(int apartmentId, CancellationToken cancellationToken = default);

    }
}
