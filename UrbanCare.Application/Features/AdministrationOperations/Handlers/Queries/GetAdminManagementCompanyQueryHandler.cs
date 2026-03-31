using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.AdministrationOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Queries
{
    public class GetAdminManagementCompanyQueryHandler : IRequestHandler<GetAdminManagementCompanyQuery, ManagementCompanyResponseDTO?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAdminManagementCompanyQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ManagementCompanyResponseDTO?> Handle(GetAdminManagementCompanyQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _employeeRepository.GetManagementCompanyByAdminAsync(request.employeeId, cancellationToken);

            if (managementCompany == null)
                return null;

            ManagementCompanyResponseDTO response = new(
                managementCompany.Id,
                managementCompany.Name,
                managementCompany.Address);

            return response;
        }
    }
}
