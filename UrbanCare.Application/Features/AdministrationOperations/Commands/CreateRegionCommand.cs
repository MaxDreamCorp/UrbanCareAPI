using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record CreateRegionCommand(int ManagementCompanyId,
        string Name,
        string CommonAddress) : IRequest<bool>;
}
