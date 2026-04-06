using MediatR;

namespace UrbanCare.Application.Features.ExecutorOperations.Commands
{
    public record UpdateStatusToOnOrderCommand(int UserId) : IRequest;
}
