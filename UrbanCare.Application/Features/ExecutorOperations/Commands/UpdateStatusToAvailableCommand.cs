using MediatR;

namespace UrbanCare.Application.Features.ExecutorOperations.Commands
{
    public record UpdateStatusToAvailableCommand(int UserId) : IRequest;
}
