using MediatR;
using UrbanCare.Application.Features.DispatcherOperations.Commands;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.DispatcherOperations.Handlers
{
    public class AppointExecutorToOrderCommandHandler : IRequestHandler<AppointExecutorToOrderCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderRepository _orderRepository;

        public AppointExecutorToOrderCommandHandler(IEmployeeRepository employeeRepository, IOrderRepository orderRepository)
        {
            _employeeRepository = employeeRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(AppointExecutorToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует");

            var dispatcher = await _employeeRepository.GetByIdAsync(request.DispatcherId, cancellationToken);
            if (dispatcher == null)
                throw new Exception("Такого диспетчера не существует");
            if (dispatcher.User.RoleId != (int)RolesEnum.Dispatcher)
                throw new Exception("Пользователь должен иметь роль \"Диспетчер\"");

            var executor = await _employeeRepository.GetByIdAsync(request.ExecutorId, cancellationToken);
            if (executor == null)
                throw new Exception("Такого исполнителя не существует");
            if (executor.User.RoleId != (int)RolesEnum.Executor)
                throw new Exception("Пользователь должен иметь роль \"Исполнитель\"");

            if (order.DispatcherId != dispatcher.Id)
                await _orderRepository.AppointDispatcherAsync(request.OrderId, dispatcher, cancellationToken);

            await _orderRepository.AppointExecutorAsync(request.OrderId, executor, cancellationToken);

            await _orderRepository.ChangeStatusAsync(request.OrderId, (int)OrderStatusEnum.ExecutorAppointed, cancellationToken);

            await _orderRepository.SetAcceptedAtDateAsync(request.OrderId, DateTime.UtcNow, cancellationToken);
        }
    }
}
