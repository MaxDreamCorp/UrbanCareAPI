using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record CreateApartmentCommand(int Number,
        int BuildingId,
        int? Entrance,
        int Floor,
        int RoomCount) : IRequest<bool>;
}
