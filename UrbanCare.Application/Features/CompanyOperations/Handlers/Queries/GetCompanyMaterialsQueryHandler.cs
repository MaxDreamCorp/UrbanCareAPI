using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetCompanyMaterialsQueryHandler : IRequestHandler<GetCompanyMaterialsQuery, List<MaterialResponseDTO>>
    {
        private readonly IMaterialRepository _materialRepository;

        public GetCompanyMaterialsQueryHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<List<MaterialResponseDTO>> Handle(GetCompanyMaterialsQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAllByManagementCompanyIdAsync(request.CompanyId, cancellationToken);

            return materials.Select(m => new MaterialResponseDTO(
                m.Id,
                new(
                    m.Storage.Id,
                    m.Storage.Name),
                m.Name,
                m.Unit,
                m.Price,
                m.AmountAtStorage)).ToList();
        }
    }
}
