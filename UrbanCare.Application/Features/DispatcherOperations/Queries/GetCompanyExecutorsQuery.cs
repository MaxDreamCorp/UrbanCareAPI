using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.DispatcherOperations.Queries
{
    public record GetCompanyExecutorsQuery(int ManagementCompanyId) : IRequest<List<ExecutorResponseDTO>?>;
}
