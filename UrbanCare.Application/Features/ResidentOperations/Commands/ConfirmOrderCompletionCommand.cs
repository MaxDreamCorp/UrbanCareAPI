using MediatR;

namespace UrbanCare.Application.Features.ResidentOperations.Commands
{
    public record ConfirmOrderCompletionCommand(
        int ResidentUserId,
        int OrderId) : IRequest;
}
