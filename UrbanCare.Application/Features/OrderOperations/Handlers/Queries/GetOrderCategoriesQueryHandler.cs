using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.OrderOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Queries
{
    public class GetOrderCategoriesQueryHandler : IRequestHandler<GetOrderCategoriesQuery, List<OrderCategoryResponseDTO>>
    {
        private readonly IOrderCategoryRepository _orderCategoryRepository;

        public GetOrderCategoriesQueryHandler(IOrderCategoryRepository orderCategoryRepository)
        {
            _orderCategoryRepository = orderCategoryRepository;
        }

        public async Task<List<OrderCategoryResponseDTO>> Handle(GetOrderCategoriesQuery request, CancellationToken cancellationToken)
        {
            var orderCategories = await _orderCategoryRepository.GetAllAsync(cancellationToken);

            return orderCategories.Select(oc => new OrderCategoryResponseDTO(
                oc.Id,
                oc.Category,
                new(
                    oc.Type.Id,
                    oc.Type.Type)))
                .ToList();
        }
    }
}
