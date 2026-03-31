using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record UpdateApartmentCommand(int Id,
        int Number,
        int BuildingId,
        int? Entrance,
        int Floor,
        int RoomCount) : IRequest<bool>;
}
