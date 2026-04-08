using MediatR;

namespace UrbanCare.Application.Features.ExecutorOperations.Commands
{
    public record AddMaterialsToOrderCommand(
        int ExecutorUserId,
        int OrderId,
        Dictionary<int, int> Materials) : IRequest;
}
