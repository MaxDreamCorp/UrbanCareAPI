using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetApartmentsByBuildingQueryHandlers : IRequestHandler<GetApartmentsByBuildingQuery, List<ApartmentResponseDTO>>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly GettingDataService _gettingDataService;

        public GetApartmentsByBuildingQueryHandlers(IApartmentRepository apartmentRepository,
                                                    IBuildingRepository buildingRepository,
                                                    GettingDataService gettingDataService)
        {
            _apartmentRepository = apartmentRepository;
            _buildingRepository = buildingRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<List<ApartmentResponseDTO>> Handle(GetApartmentsByBuildingQuery request, CancellationToken cancellationToken)
        {
            var building = await _buildingRepository.GetByIdAsync(request.BuildingId, cancellationToken);

            if (building == null)
                throw new Exception("Данного здания не существует");

            var apartments = await _apartmentRepository.GetByBuildingIdAsync(building.Id, cancellationToken);

            List<ApartmentResponseDTO> apartmentDTOs = new List<ApartmentResponseDTO>();

            if (apartments == null)
                return apartmentDTOs;

            apartmentDTOs.AddRange(apartments.Select(async a => await _gettingDataService.GetApartmentResponseDTOByApartmentIdAsync(a.Id))
                .Select(a => a.Result));

            return apartmentDTOs;
        }
    }
}
