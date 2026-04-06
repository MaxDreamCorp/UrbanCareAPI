using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.ExecutorOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ExecutorOperations.Handlers.Queries
{
    public class GetExecutorOrdersQueryHandler : IRequestHandler<GetExecutorOrdersQuery, List<OrderResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly GettingDataService _gettingDataService;

        public GetExecutorOrdersQueryHandler(IOrderRepository orderRepository, IEmployeeRepository employeeRepository, GettingDataService gettingDataService)
        {
            _orderRepository = orderRepository;
            _employeeRepository = employeeRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<List<OrderResponseDTO>> Handle(GetExecutorOrdersQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByUserAsync(request.UserId, cancellationToken);
            if (employee == null)
                throw new Exception("Такого сотрудника не существует");

            var orders = await _orderRepository.GetByExecutorIdAsync(employee.Id, cancellationToken);

            var orderDTOs = orders.Select(async o => await _gettingDataService.GetOrderResponseDTOByOrderIdAsync(o.Id));
            return orderDTOs.Select(o => o.Result).ToList();
        }
    }
}
