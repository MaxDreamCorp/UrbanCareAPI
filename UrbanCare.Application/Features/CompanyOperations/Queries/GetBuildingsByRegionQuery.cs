using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.CompanyOperations.Queries
{
    public record GetBuildingsByRegionQuery(int RegionId) : IRequest<List<BuildingResponseDTO>>;
}
