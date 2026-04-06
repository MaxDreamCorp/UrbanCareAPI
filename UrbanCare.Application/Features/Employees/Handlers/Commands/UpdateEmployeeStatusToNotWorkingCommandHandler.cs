using MediatR;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Commands
{
    public class UpdateEmployeeStatusToNotWorkingCommandHandler : IRequestHandler<UpdateEmployeeStatusToNotWorkingCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeStatusToNotWorkingCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateEmployeeStatusToNotWorkingCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateStatusByUserIdAsync(request.UserId, (int)EmployeeStatusEnum.NotWorking, cancellationToken);
        }
    }
}
