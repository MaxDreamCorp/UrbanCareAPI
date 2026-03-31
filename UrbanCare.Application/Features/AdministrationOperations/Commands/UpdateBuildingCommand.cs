using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record UpdateBuildingCommand(int Id,
        string Number,
        string Address,
        int RegionId,
        int BuildingTypeId,
        short YearBuit,
        int FloorCount,
        int WallMaterialId,
        int FloorMaterialId) : IRequest<bool>;
}
