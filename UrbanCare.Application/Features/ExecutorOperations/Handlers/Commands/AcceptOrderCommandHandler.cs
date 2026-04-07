using MediatR;
using UrbanCare.Application.Features.ExecutorOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ExecutorOperations.Handlers.Commands
{
    public class AcceptOrderCommandHandler : IRequestHandler<AcceptOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AcceptOrderCommandHandler(IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
        {
            _orderRepository = orderRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует.");

            var executor = await _employeeRepository.GetByUserAsync(request.ExecutorUserId, cancellationToken);
            if (executor == null)
                throw new Exception("Такого исполнителя не существует.");

            if (order.StatusId != (int)Domain.Enums.OrderStatusEnum.ExecutorAppointed)
                throw new Exception("Заказ не находится в статусе 'Исполнитель назначен'.");

            var isOnTheOrder = await _orderRepository.CheckIfExecutorIsAppointedToOrderAsync(request.OrderId, executor.Id, cancellationToken);

            if (!isOnTheOrder)
                throw new Exception("Исполнитель не назначен на этот заказ.");

            await _orderRepository.ChangeStatusAsync(request.OrderId, (int)Domain.Enums.OrderStatusEnum.InProgress, cancellationToken);
        }
    }
}
