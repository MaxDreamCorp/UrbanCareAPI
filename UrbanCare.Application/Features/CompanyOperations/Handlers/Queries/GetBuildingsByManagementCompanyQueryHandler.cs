using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetBuildingsByManagementCompanyQueryHandler : IRequestHandler<GetBuildingsByManagementCompanyQuery, List<BuildingResponseDTO>>
    {
        private readonly IManagementCompanyRepository _managementCompanyRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly GettingDataService _gettingDataService;

        public GetBuildingsByManagementCompanyQueryHandler(IManagementCompanyRepository managementCompanyRepository, IBuildingRepository buildingRepository, GettingDataService gettingDataService)
        {
            _managementCompanyRepository = managementCompanyRepository;
            _buildingRepository = buildingRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<List<BuildingResponseDTO>> Handle(GetBuildingsByManagementCompanyQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var buildings = await _buildingRepository.GetByManagementCompanyIdAsync(request.ManagementCompanyId, cancellationToken);

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
