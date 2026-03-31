using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.ResidentOperations.Queries
{
    public record GetResidentByUserIdQuery(int UserId) : IRequest<ResidentResponseDTO?>;
}
