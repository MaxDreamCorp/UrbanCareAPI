using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.ExecutorOperations.Queries
{
    public record GetExecutorOrdersQuery(int UserId) : IRequest<List<OrderResponseDTO>>;
}
