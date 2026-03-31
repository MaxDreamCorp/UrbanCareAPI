using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.AdministrationOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Queries
{
    public class GetManagementCompanyEmployeesQueryHandler : IRequestHandler<GetManagementCompanyEmployeesQuery, List<ReportEmployeeInformationResponseDTO>?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetManagementCompanyEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<ReportEmployeeInformationResponseDTO>?> Handle(GetManagementCompanyEmployeesQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _employeeRepository.GetManagementCompanyByAdminAsync(request.AdminId, cancellationToken);
            if (managementCompany == null)
                return null;

            var employees = await _employeeRepository.GetByManagementCompanyIdAsync(managementCompany.Id, cancellationToken);

            if (employees == null)
                return null;

            var response = new List<ReportEmployeeInformationResponseDTO>();

            foreach (var employee in employees)
            {
                response.Add(new(
                    employee.Id,
                    employee.User.Fullname,
                    employee.User.Email,
                    employee.User.Phone,
                    employee.EmployeePosition.Name,
                    employee.QualificationCategory.Name,
                    employee.Status.Status,
                    employee.ExperienceYears,
                    employee.Salary,
                    employee.Notes));
            }

            return response;
        }
    }
}
