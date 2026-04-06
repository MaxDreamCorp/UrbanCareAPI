using MediatR;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Commands
{
    public class UpdateEmployeeStatusToWorkingCommandHandler : IRequestHandler<UpdateEmployeeStatusToWorkingCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeStatusToWorkingCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateEmployeeStatusToWorkingCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateStatusByUserIdAsync(request.UserId, (int)EmployeeStatusEnum.Working, cancellationToken);
        }
    }
}
