using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetRegionsByManagementCompanyQueryHandler : IRequestHandler<GetRegionsByManagementCompanyQuery, List<RegionResponseDTO>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public GetRegionsByManagementCompanyQueryHandler(IRegionRepository regionRepository, IManagementCompanyRepository managementCompanyRepository)
        {
            _regionRepository = regionRepository;
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<List<RegionResponseDTO>> Handle(GetRegionsByManagementCompanyQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var regions = await _regionRepository.GetByManagementCompanyIdAsync(request.ManagementCompanyId, cancellationToken);

            var regionsDTO = new List<RegionResponseDTO>();
            var managementCompanyDTO = new ManagementCompanyResponseDTO(
                managementCompany.Id,
                managementCompany.Name,
                managementCompany.Address);

            if (regions != null)
                regionsDTO = regions.Select(r => new RegionResponseDTO(
                    r.Id,
                    r.Name,
                    r.CommonAddress,
                    managementCompanyDTO)).ToList();

            return regionsDTO;
        }
    }
}
