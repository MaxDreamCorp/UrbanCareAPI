using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetFloorMaterialsQueryHandler : IRequestHandler<GetFloorMaterialsQuery, List<FloorMaterialResponseDTO>>
    {
        private readonly IFloorMaterialRepository _floorMaterialRepository;

        public GetFloorMaterialsQueryHandler(IFloorMaterialRepository floorMaterialRepository)
        {
            _floorMaterialRepository = floorMaterialRepository;
        }

        public async Task<List<FloorMaterialResponseDTO>> Handle(GetFloorMaterialsQuery request, CancellationToken cancellationToken)
        {
            return (await _floorMaterialRepository.GetAllAsync(cancellationToken))?
                .Select(m => new FloorMaterialResponseDTO(m.Id, m.Name)).ToList()
                ?? new();
        }
    }
}
