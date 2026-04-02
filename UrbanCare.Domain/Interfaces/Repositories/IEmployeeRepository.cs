using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
        Task RemoveAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee?> GetByUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<List<Employee>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<Employee>?> GetExecutorsByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<ManagementCompany?> GetManagementCompanyByAdminAsync(int adminId, CancellationToken cancellationToken = default);
        Task<Employee?> GetAdminByManagementCompanyAsync(int managementCompanyId, CancellationToken cancellationToken = default);
        Task<int> GetExecutorActiveTasksCountAsync(int executorId, CancellationToken cancellationToken = default);
        Task<int> GetExecutorFinishedTasksCountAsync(int executorId, CancellationToken cancellationToken = default);
    }
}
