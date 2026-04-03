using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.DispatcherOperations.Queries
{
    public record GetCompanyOrdersQuery(int ManagementCompanyId) : IRequest<List<OrderResponseDTO>>;
}
