using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.Employees.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Queries
{
    public class GetAllEmployeePositionsQueryHandler : IRequestHandler<GetAllEmployeePositionsQuery, List<EmployeePositionResponseDTO>>
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;

        public GetAllEmployeePositionsQueryHandler(IEmployeePositionRepository employeePositionRepository)
        {
            _employeePositionRepository = employeePositionRepository;
        }

        public async Task<List<EmployeePositionResponseDTO>> Handle(GetAllEmployeePositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = await _employeePositionRepository.GetAllAsync(cancellationToken);
            return positions.Select(p => new EmployeePositionResponseDTO(p.Id, p.Name, p.Description)).ToList();
        }
    }
}
