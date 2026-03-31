using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.Employees.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Queries
{
    public class GetAllEmployeeStatusesQueryHandler : IRequestHandler<GetAllEmployeeStatusesQuery, List<EmployeeStatusResponseDTO>>
    {
        private readonly IEmployeeStatusRepository _employeeStatusRepository;

        public GetAllEmployeeStatusesQueryHandler(IEmployeeStatusRepository employeeStatusRepository)
        {
            _employeeStatusRepository = employeeStatusRepository;
        }

        public async Task<List<EmployeeStatusResponseDTO>> Handle(GetAllEmployeeStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _employeeStatusRepository.GetAllAsync(cancellationToken);
            return statuses.Select(s => new EmployeeStatusResponseDTO(s.Id, s.Status)).ToList();
        }
    }
}
