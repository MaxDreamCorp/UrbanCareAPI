using MediatR;

namespace UrbanCare.Application.Features.ResidentOperations.Commands
{
    public record CreateResidentCommand(int UserId,
        int ApartmentId,
        DateOnly MovingIntoDate) : IRequest<bool>;
}
