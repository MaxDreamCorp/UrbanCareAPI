using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetBuildingsByRegionQueryHandler : IRequestHandler<GetBuildingsByRegionQuery, List<BuildingResponseDTO>>
    {
        private readonly GettingDataService _gettingDataService;
        private readonly IBuildingRepository _buildingRepository;

        public GetBuildingsByRegionQueryHandler(GettingDataService gettingDataService, IBuildingRepository buildingRepository)
        {
            _gettingDataService = gettingDataService;
            _buildingRepository = buildingRepository;
        }

        public async Task<List<BuildingResponseDTO>> Handle(GetBuildingsByRegionQuery request, CancellationToken cancellationToken)
        {
            var buildings = await _buildingRepository.GetByRegionIdAsync(request.RegionId, cancellationToken);

            List<BuildingResponseDTO> results = new List<BuildingResponseDTO>();

            if (buildings == null)
                return results;


            results.AddRange(buildings.Select(async b =>
                await _gettingDataService.GetBuildingResponseDTOByBuildingIdAsync(b.Id))
                .Select(b => b.Result));

            return results;
        }
    }
}
