using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetBuildingTypesQueryHandler : IRequestHandler<GetBuildingTypesQuery, List<BuildingTypeResponseDTO>>
    {
        private readonly IBuildingTypeRepository _buildingTypeRepository;

        public GetBuildingTypesQueryHandler(IBuildingTypeRepository buildingTypeRepository)
        {
            _buildingTypeRepository = buildingTypeRepository;
        }

        public async Task<List<BuildingTypeResponseDTO>> Handle(GetBuildingTypesQuery request, CancellationToken cancellationToken)
        {
            return (await _buildingTypeRepository.GetAllAsync(cancellationToken))?
                .Select(bt => new BuildingTypeResponseDTO(bt.Id, bt.Type)).ToList()
                ?? new();
        }
    }
}
