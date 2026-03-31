using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleResponseDTO>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleResponseDTO>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return (await _roleRepository.GetAllAsync(cancellationToken))?
                .Select(r => new RoleResponseDTO(r.Id, r.Role1)).ToList()
                ?? new();
        }
    }
}
