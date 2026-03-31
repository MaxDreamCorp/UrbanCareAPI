using MediatR;
using UrbanCare.Application.Features.OrderOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IOrderCategoryRepository _orderCategoryRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IOrderStatusRepository orderStatusRepository,
                                         IOrderCategoryRepository orderCategoryRepository,
                                         IPriorityRepository priorityRepository,
                                         IResidentRepository residentRepository,
                                         IBuildingRepository buildingRepository,
                                         IApartmentRepository apartmentRepository)
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _orderCategoryRepository = orderCategoryRepository;
            _priorityRepository = priorityRepository;
            _residentRepository = residentRepository;
            _buildingRepository = buildingRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var resident = await _residentRepository.GetByIdAsync(request.ResidentId, cancellationToken);
            if (resident == null)
                throw new Exception("Данного жителя не существует");

            var orderCategory = await _orderCategoryRepository.GetByIdAsync(request.OrderCategoryId, cancellationToken);
            if (orderCategory == null)
                throw new Exception("Данной категории заказа не существует");

            var building = await _buildingRepository.GetByIdAsync(request.BuildingId, cancellationToken);
            if (building == null)
                throw new Exception("Данного здания не существует");

            Apartment? apartment = null;
            if (request.ApartmentId.HasValue)
            {
                apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId.Value, cancellationToken);
                if (apartment == null)
                    throw new Exception("Данной квартиры не существует");
            }

            var priority = await _priorityRepository.GetByIdAsync(request.PriorityId, cancellationToken);
            if (priority == null)
                throw new Exception("Данного приоритета не существует");

            var newOrderStatus = await _orderStatusRepository.GetByIdAsync((int)OrderStatusEnum.New, cancellationToken);
            if (newOrderStatus == null)
                throw new Exception("Данного статуса заказа не существует");

            var order = Order.Create(
                await _orderRepository.GetNextIdAsync(cancellationToken),
                resident,
                request.Description,
                orderCategory,
                building,
                apartment,
                priority,
                request.ContactPhone,
                request.ContactEmail,
                newOrderStatus);

            await _orderRepository.AddAsync(order, cancellationToken);
            return true;
        }
    }
}
