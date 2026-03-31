using MediatR;

namespace UrbanCare.Application.Features.OrderOperations.Commands
{
    public record CreateOrderCommand(
        int ResidentId,
        string Description,
        int OrderCategoryId,
        int BuildingId,
        int? ApartmentId,
        int PriorityId,
        string ContactPhone,
        string ContactEmail) : IRequest<bool>;
}
