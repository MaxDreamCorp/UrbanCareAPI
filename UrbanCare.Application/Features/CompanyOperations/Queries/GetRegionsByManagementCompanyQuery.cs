using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.CompanyOperations.Queries
{
    public record GetRegionsByManagementCompanyQuery(int ManagementCompanyId) : IRequest<List<RegionResponseDTO>>;
}
