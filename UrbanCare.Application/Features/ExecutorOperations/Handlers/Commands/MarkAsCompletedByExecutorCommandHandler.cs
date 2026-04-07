using MediatR;
using UrbanCare.Application.Features.ExecutorOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ExecutorOperations.Handlers.Commands
{
    public class MarkAsCompletedByExecutorCommandHandler : IRequestHandler<MarkAsCompletedByExecutorCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public MarkAsCompletedByExecutorCommandHandler(IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
        {
            _orderRepository = orderRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(MarkAsCompletedByExecutorCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Такого заказа не существует.");

            var executor = await _employeeRepository.GetByUserAsync(request.ExecutorUserId, cancellationToken);
            if (executor == null)
                throw new Exception("Такого исполнителя не существует.");

            if (order.StatusId != (int)Domain.Enums.OrderStatusEnum.InProgress)
                throw new Exception("Заказ не находится в статусе 'В работе'.");

            var isOnTheOrder = await _orderRepository.CheckIfExecutorIsAppointedToOrderAsync(request.OrderId, executor.Id, cancellationToken);

            if (!isOnTheOrder)
                throw new Exception("Исполнитель не назначен на этот заказ.");

            await _orderRepository.MarkAsCompletedByExecutorAsync(request.OrderId, cancellationToken);
        }
    }
}
