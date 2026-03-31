using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.OrderOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Queries
{
    public class GetOrdersByResidentIdHandler : IRequestHandler<GetOrdersByResidentId, List<OrderResponseDTO>>
    {
        private readonly GettingDataService _gettingDataService;
        private readonly IOrderRepository _orderRepository;
        private readonly IResidentRepository _residentRepository;

        public GetOrdersByResidentIdHandler(GettingDataService gettingDataService, IOrderRepository orderRepository, IResidentRepository residentRepository)
        {
            _gettingDataService = gettingDataService;
            _orderRepository = orderRepository;
            _residentRepository = residentRepository;
        }

        public async Task<List<OrderResponseDTO>> Handle(GetOrdersByResidentId request, CancellationToken cancellationToken)
        {
            var resident = await _residentRepository.GetByIdAsync(request.ResidentId, cancellationToken);
            if (resident == null)
                throw new Exception("Данного жителя не существует");

            var orders = await _orderRepository.GetByResidentIdAsync(resident.Id, cancellationToken);

            var orderDTOs = orders.Select(async o => await _gettingDataService.GetOrderResponseDTOByOrderIdAsync(o.Id));
            return orderDTOs.Select(o => o.Result).ToList();
        }
    }
}
