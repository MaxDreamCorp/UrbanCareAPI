using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.DispatcherOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.DispatcherOperations.Handlers.Queries
{
    public class GetCompanyInProgressOrdersQueryHandler : IRequestHandler<GetCompanyInProgressOrdersQuery, List<OrderResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly GettingDataService _gettingDataService;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public GetCompanyInProgressOrdersQueryHandler(IOrderRepository orderRepository, GettingDataService gettingDataService, IManagementCompanyRepository managementCompanyRepository)
        {
            _orderRepository = orderRepository;
            _gettingDataService = gettingDataService;
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<List<OrderResponseDTO>> Handle(GetCompanyInProgressOrdersQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);
            if (managementCompany == null)
                throw new Exception("Управляющая компания не найдена");

            var orders = await _orderRepository.GetInProgressByManagementCompanyIdAsync(request.ManagementCompanyId, cancellationToken);

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
