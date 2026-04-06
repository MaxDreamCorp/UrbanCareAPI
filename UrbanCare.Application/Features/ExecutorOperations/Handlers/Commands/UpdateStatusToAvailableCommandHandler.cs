using MediatR;
using UrbanCare.Application.Features.ExecutorOperations.Commands;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ExecutorOperations.Handlers.Commands
{
    public class UpdateStatusToAvailableCommandHandler : IRequestHandler<UpdateStatusToAvailableCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateStatusToAvailableCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateStatusToAvailableCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByUserAsync(request.UserId, cancellationToken);
            if (employee == null)
                throw new Exception("Такого сотрудника не существует");
            if (employee.User.RoleId != (int)RolesEnum.Executor)
                throw new Exception("Пользователь должен иметь роль \"Исполнитель\"");

            await _employeeRepository.UpdateStatusByUserIdAsync(request.UserId, (int)EmployeeStatusEnum.Available, cancellationToken);
        }
    }

}
