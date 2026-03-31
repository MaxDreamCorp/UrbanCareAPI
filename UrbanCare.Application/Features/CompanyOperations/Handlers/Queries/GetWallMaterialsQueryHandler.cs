using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetWallMaterialsQueryHandler : IRequestHandler<GetWallMaterialsQuery, List<WallMaterialResponseDTO>>
    {
        private readonly IWallMaterialRepository _wallMaterialRepository;

        public GetWallMaterialsQueryHandler(IWallMaterialRepository wallMaterialRepository)
        {
            _wallMaterialRepository = wallMaterialRepository;
        }

        public async Task<List<WallMaterialResponseDTO>> Handle(GetWallMaterialsQuery request, CancellationToken cancellationToken)
        {
            return (await _wallMaterialRepository.GetAllAsync(cancellationToken))?
                .Select(m => new WallMaterialResponseDTO(m.Id, m.Name)).ToList()
                ?? new();
        }
    }
}
