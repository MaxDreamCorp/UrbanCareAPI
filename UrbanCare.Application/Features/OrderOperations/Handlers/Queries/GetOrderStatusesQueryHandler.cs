using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.OrderOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Queries
{
    public class GetOrderStatusesQueryHandler : IRequestHandler<GetOrderStatusesQuery, List<OrderStatusResponseDTO>>
    {
        private readonly IOrderStatusRepository _orderStatusRepository;

        public GetOrderStatusesQueryHandler(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<List<OrderStatusResponseDTO>> Handle(GetOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            var orderStatuses = await _orderStatusRepository.GetAllAsync(cancellationToken);

            return orderStatuses.Select(os => new OrderStatusResponseDTO(
                os.Id,
                os.Status))
                .ToList();
        }
    }
}
