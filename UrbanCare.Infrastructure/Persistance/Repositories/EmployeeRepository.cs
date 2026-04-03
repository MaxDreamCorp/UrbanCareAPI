using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ManagementCompany?> GetManagementCompanyByAdminAsync(int adminId, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.Include(x => x.ManagementCompany).FirstOrDefaultAsync(x => x.Id == adminId);
            if (employee == null) return null;
            return employee.ManagementCompany;
        }

        public Task<Employee?> GetByUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            return _context.Employees.Include(e => e.User).FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);
        }

        public async Task<List<Employee>?> GetByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(e => e.User)
                    .ThenInclude(u => u.UserPersonalData)
                .Include(e => e.ManagementCompany)
                .Include(e => e.EmployeePosition)
                .Include(e => e.QualificationCategory)
                .Include(e => e.Status)
                .Where(e => e.ManagementCompanyId == companyId)
                .ToListAsync();
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Employees.AnyAsync())
                return await _context.Employees.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Employee?> GetAdminByManagementCompanyAsync(int managementCompanyId, CancellationToken cancellationToken = default)
        {
            var managementCompany = await _context.ManagementCompanies.FindAsync(managementCompanyId, cancellationToken);

            if (managementCompany == null) return null;

            var admin = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.ManagementCompanyId == managementCompany.Id &&
                e.User.RoleId == 2);

            return admin;
        }

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(e => e.ManagementCompany)
                .Include(e => e.EmployeePosition)
                .Include(e => e.Status)
                .Include(e => e.QualificationCategory)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        }

        public async Task<List<Employee>?> GetExecutorsByManagementCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(e => e.ManagementCompany)
                .Include(e => e.EmployeePosition)
                .Include(e => e.QualificationCategory)
                .Include(e => e.Status)
                .Where(e => e.ManagementCompanyId == companyId && e.User.RoleId == (int)RolesEnum.Executor)
                .ToListAsync();
        }

        public async Task<int> GetExecutorActiveTasksCountAsync(int executorId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.CountAsync(o => o.Status.Id == (int)OrderStatusEnum.InProgress
                && o.OrderExecutors.Any(oe => oe.Id == executorId), cancellationToken);
        }

        public async Task<int> GetExecutorCompletedTasksCountAsync(int executorId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.CountAsync(o => o.Status.Id == (int)OrderStatusEnum.Completed
               && o.OrderExecutors.Any(oe => oe.Id == executorId), cancellationToken);
        }
    }
}
