using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.DispatcherOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.DispatcherOperations.Handlers.Queries
{
    public class GetCompanyOrdersQueryHandler : IRequestHandler<GetCompanyOrdersQuery, List<OrderResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;
        private readonly GettingDataService _gettingDataService;

        public GetCompanyOrdersQueryHandler(IOrderRepository orderRepository, IManagementCompanyRepository managementCompanyRepository, GettingDataService gettingDataService)
        {
            _orderRepository = orderRepository;
            _managementCompanyRepository = managementCompanyRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<List<OrderResponseDTO>> Handle(GetCompanyOrdersQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);
            if (managementCompany == null)
                throw new Exception("Управляющая компания не найдена");

            var orders = await _orderRepository.GetByManagementCompanyIdAsync(request.ManagementCompanyId, cancellationToken);

            var orderResponseDTOs = new List<OrderResponseDTO>();
            foreach (var order in orders)
            {
                var orderResponseDTO = await _gettingDataService.GetOrderResponseDTOByOrderIdAsync(order.Id, cancellationToken);
                orderResponseDTOs.Add(orderResponseDTO);
            }
            return orderResponseDTOs;
        }
    }
}
