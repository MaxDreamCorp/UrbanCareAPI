using MediatR;

namespace UrbanCare.Application.Features.DispatcherOperations.Commands
{
    public record AppointExecutorToOrderCommand(int OrderId,
        int DispatcherId,
        int ExecutorId) : IRequest;
}
