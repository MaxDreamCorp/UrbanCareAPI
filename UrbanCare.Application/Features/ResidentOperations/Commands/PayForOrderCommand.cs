using MediatR;

namespace UrbanCare.Application.Features.ResidentOperations.Commands
{
    public record PayForOrderCommand(int ResidentUserId, int OrderId) : IRequest;
}
