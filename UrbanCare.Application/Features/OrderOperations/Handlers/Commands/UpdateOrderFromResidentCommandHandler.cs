using MediatR;
using UrbanCare.Application.Features.OrderOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Commands
{
    public class UpdateOrderFromResidentCommandHandler : IRequestHandler<UpdateOrderFromResidentCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderCategoryRepository _orderCategoryRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public UpdateOrderFromResidentCommandHandler(IOrderRepository orderRepository,
                                         IOrderCategoryRepository orderCategoryRepository,
                                         IPriorityRepository priorityRepository,
                                         IBuildingRepository buildingRepository,
                                         IApartmentRepository apartmentRepository)
        {
            _orderRepository = orderRepository;
            _orderCategoryRepository = orderCategoryRepository;
            _priorityRepository = priorityRepository;
            _buildingRepository = buildingRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<bool> Handle(UpdateOrderFromResidentCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order == null)
                throw new Exception("Данного заказа не существует");

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


            var orderForUpdate = Order.CreateForUpdate(
                order.Id,
                order.Resident,
                request.Description,
                orderCategory,
                building,
                apartment,
                priority,
                request.ContactPhone,
                request.ContactEmail,
                order.Status,
                order.CreatedAt);

            await _orderRepository.UpdateAsync(orderForUpdate, cancellationToken);
            return true;
        }
    }
}
