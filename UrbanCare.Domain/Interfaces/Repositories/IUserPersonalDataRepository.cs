using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IUserPersonalDataRepository
    {
        Task AddAsync(UserPersonalDatum userPersonalData, CancellationToken cancellationToken = default);
        Task RemoveAsync(UserPersonalDatum userPersonalData, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
    }
}
