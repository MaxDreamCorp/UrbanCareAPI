using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.Employees.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Queries
{
    public class GetEmployeeByUserIdQueryHandler : IRequestHandler<GetEmployeeByUserIdQuery, EmployeeDataResponseDTO?>
    {
        private readonly GettingDataService _gettingDataService;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByUserIdQueryHandler(GettingDataService gettingDataService, IEmployeeRepository employeeRepository)
        {
            _gettingDataService = gettingDataService;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDataResponseDTO?> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByUserAsync(request.UserId, cancellationToken);
            if (employee == null)
                return null;

            return await _gettingDataService.GetEmployeeDataResponseDTOByEmployeeIdAsync(employee.Id, cancellationToken);
        }
    }
}
