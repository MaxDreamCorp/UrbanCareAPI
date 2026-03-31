using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record DeleteRegionCommand(int Id) : IRequest<bool>;
}
