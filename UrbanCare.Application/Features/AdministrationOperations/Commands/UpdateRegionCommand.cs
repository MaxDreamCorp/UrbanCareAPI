using MediatR;

namespace UrbanCare.Application.Features.AdministrationOperations.Commands
{
    public record UpdateRegionCommand(int Id,
        string Name,
        string CommonAddress,
        int ManagementCompanyId) : IRequest<bool>;
}
