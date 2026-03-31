using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record DeleteApartmentCommand(int Id) : IRequest<bool>;
}
