using MediatR;

namespace UrbanCare.Application.Features.UserOperations.Queries
{
    public record CheckIsEmployeeQuery(int UserId) : IRequest<bool>;
}
