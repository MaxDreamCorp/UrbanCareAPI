using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.Employees.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Queries
{
    public class GetEmployeeByUserIdQueryHandler : IRequestHandler<GetEmployeeByUserIdQuery, EmployeeDataResponseDTO?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByUserIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDataResponseDTO?> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.UserId);

            if (employee == null) 
                return null;

            return new EmployeeDataResponseDTO(
                employee.Id,
                request.UserId,
                new(employee.ManagementCompany.Id,
                    employee.ManagementCompany.Name,
                    employee.ManagementCompany.Address),
                new (employee.EmployeePosition.Id,
                    employee.EmployeePosition.Name,
                    employee.EmployeePosition.Description),
                new(employee.Status.Id, 
                    employee.Status.Status),
                new (employee.QualificationCategory.Id,
                    employee.QualificationCategory.Name,
                    employee.QualificationCategory.Code,
                    employee.QualificationCategory.MinExperienceYears,
                    employee.QualificationCategory.SalaryCoefficient),
                employee.EmploymentDate,
                employee.ExperienceYears,
                employee.Salary,
                employee.Notes
                );
        }
    }
}
