using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IEmployeePositionRepository
    {
        Task<EmployeePosition?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<EmployeePosition>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
