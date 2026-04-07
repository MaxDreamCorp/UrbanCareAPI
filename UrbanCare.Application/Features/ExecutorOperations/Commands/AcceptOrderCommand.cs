using MediatR;

namespace UrbanCare.Application.Features.ExecutorOperations.Commands
{
    public record AcceptOrderCommand(int ExecutorUserId, int OrderId) : IRequest;
}
