using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, List<RegionResponseDTO>>
    {
        private readonly IRegionRepository _regionRepository;

        public GetAllRegionsQueryHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<List<RegionResponseDTO>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = await _regionRepository.GetAllAsync(cancellationToken);

            List<RegionResponseDTO> regionDTOs = regions?.Select(r => new RegionResponseDTO(
                r.Id,
                r.Name,
                r.CommonAddress,
                new(
                    r.ManagementCompany.Id,
                    r.ManagementCompany.Name,
                    r.ManagementCompany.Address))).ToList()
                    ?? new();

            return regionDTOs;
        }
    }
}
