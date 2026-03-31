using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.UserOperations.Queries
{
    public record GetUserDataQuery(int UserId) : IRequest<UserDataResponseDTO?>;
}
