using MediatR;
using UrbanCare.Application.Features.ExecutorOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ExecutorOperations.Handlers.Commands
{
    public class AddMaterialsToOrderCommandHandler : IRequestHandler<AddMaterialsToOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddMaterialsToOrderCommandHandler(IOrderRepository orderRepository, IMaterialRepository materialRepository, IEmployeeRepository employeeRepository)
        {
            _orderRepository = orderRepository;
            _materialRepository = materialRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(AddMaterialsToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new Exception("Данного заказа не существует");

            var executor = await _employeeRepository.GetByUserAsync(request.ExecutorUserId, cancellationToken);
            if (executor == null)
                throw new Exception("Данного исполнителя не существует");

            if (!await _orderRepository.CheckIfExecutorIsAppointedToOrderAsync(request.OrderId, executor.Id, cancellationToken))
                throw new Exception("Исполнитель не назначен на данный заказ");

            foreach (var materialIdQuantityPair in request.Materials)
            {
                var material = await _materialRepository.GetByIdAsync(materialIdQuantityPair.Key, cancellationToken);
                if (material == null)
                    throw new Exception($"Материал с id {materialIdQuantityPair.Key} не найден");

                _orderRepository.AddMaterialAsync(order.Id, material, materialIdQuantityPair.Value, cancellationToken).Wait(cancellationToken);
            }
        }
    }
}
