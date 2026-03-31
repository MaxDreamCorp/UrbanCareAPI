using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IManagementCompanyRepository
    {
        Task AddAsync(ManagementCompany managementCompany, CancellationToken cancellationToken = default);
        Task RemoveAsync(ManagementCompany managementCompany, CancellationToken cancellationToken = default);
        Task<ManagementCompany?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<List<ManagementCompany>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
