using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IEmployeeStatusRepository
    {
        Task<EmployeeStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<EmployeeStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
