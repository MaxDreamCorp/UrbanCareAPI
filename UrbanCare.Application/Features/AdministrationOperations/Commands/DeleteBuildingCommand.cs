using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record DeleteBuildingCommand(int Id) : IRequest<bool>;
}
