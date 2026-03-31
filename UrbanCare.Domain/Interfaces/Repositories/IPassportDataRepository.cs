using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IPassportDataRepository
    {
        Task AddAsync(PassportDatum passportData, CancellationToken cancellationToken = default);
        Task RemoveAsync(PassportDatum passportData, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<PassportDatum?> GetBySeriaAndNumberAsync(string seria, string number, CancellationToken cancellationToken = default);
    }
}
