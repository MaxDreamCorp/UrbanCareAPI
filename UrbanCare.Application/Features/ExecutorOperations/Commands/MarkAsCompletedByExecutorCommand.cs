using MediatR;

namespace UrbanCare.Application.Features.ExecutorOperations.Commands
{
    public record MarkAsCompletedByExecutorCommand(int ExecutorUserId, int OrderId) : IRequest;
}
