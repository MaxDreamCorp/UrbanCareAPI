using MediatR;

namespace UrbanCare.Application.Features.OrderOperations.Commands
{
    public record UpdateOrderFromResidentCommand(int Id,
        string Description,
        int OrderCategoryId,
        int BuildingId,
        int? ApartmentId,
        int PriorityId,
        string ContactPhone,
        string ContactEmail) : IRequest<bool>;
}
