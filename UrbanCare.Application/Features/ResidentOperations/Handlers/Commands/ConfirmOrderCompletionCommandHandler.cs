using MediatR;
using UrbanCare.Application.Features.ResidentOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ResidentOperations.Handlers.Commands
{
    public class ConfirmOrderCompletionCommandHandler : IRequestHandler<ConfirmOrderCompletionCommand>
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IOrderRepository _orderRepository;

        public ConfirmOrderCompletionCommandHandler(IResidentRepository residentRepository, IOrderRepository orderRepository)
        {
            _residentRepository = residentRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(ConfirmOrderCompletionCommand request, CancellationToken cancellationToken)
        {
            var resident = await _residentRepository.GetByUserIdAsync(request.ResidentUserId, cancellationToken);
            if (resident == null)
                throw new Exception("Такого жителя не существует.");

            await _orderRepository.ConfirmCompletionByResidentAsync(request.OrderId, resident, cancellationToken);
        }
    }
}
