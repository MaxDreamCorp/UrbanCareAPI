using MediatR;
using UrbanCare.Application.Features.ResidentOperations.Commands;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ResidentOperations.Handlers.Commands
{
    public class PayForOrderCommandHandler : IRequestHandler<PayForOrderCommand>
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IOrderRepository _orderRepository;

        public PayForOrderCommandHandler(IResidentRepository residentRepository, IOrderRepository orderRepository)
        {
            _residentRepository = residentRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(PayForOrderCommand request, CancellationToken cancellationToken)
        {
            var resident = await _residentRepository.GetByUserIdAsync(request.ResidentUserId);
            if (resident == null)
                throw new Exception("Такого жителя не существует.");

            await _orderRepository.ExpendMaterialsAsync(request.OrderId, cancellationToken);
            await _orderRepository.ChangeStatusAsync(request.OrderId, (int)OrderStatusEnum.Completed, cancellationToken);
        }
    }
}
