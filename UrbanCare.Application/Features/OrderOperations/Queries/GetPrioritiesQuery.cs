using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.OrderOperations.Queries
{
    public record GetPrioritiesQuery() : IRequest<List<PriorityResponseDTO>>;
}
