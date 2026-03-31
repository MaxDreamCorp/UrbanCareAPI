using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.CompanyOperations.Queries
{
    public record GetBuildingsByManagementCompanyQuery(int ManagementCompanyId) : IRequest<List<BuildingResponseDTO>>;
}
