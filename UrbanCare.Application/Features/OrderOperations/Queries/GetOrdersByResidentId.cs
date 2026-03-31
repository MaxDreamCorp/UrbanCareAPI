using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.OrderOperations.Queries
{
    public record GetOrdersByResidentId(int ResidentId) : IRequest<List<OrderResponseDTO>>;
}
