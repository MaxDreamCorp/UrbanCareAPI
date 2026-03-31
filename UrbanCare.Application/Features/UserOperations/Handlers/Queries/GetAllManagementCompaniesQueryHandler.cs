using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.UserOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.UserOperations.Handlers.Queries
{
    public class GetAllManagementCompaniesQueryHandler : IRequestHandler<GetAllManagementCompaniesQuery, List<ManagementCompanyResponseDTO>>
    {
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public GetAllManagementCompaniesQueryHandler(IManagementCompanyRepository managementCompanyRepository)
        {
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<List<ManagementCompanyResponseDTO>> Handle(GetAllManagementCompaniesQuery request, CancellationToken cancellationToken)
        {
            return (await _managementCompanyRepository.GetAllAsync(cancellationToken))
                 .Select(mc => new ManagementCompanyResponseDTO(mc.Id, mc.Name, mc.Address))
                 .ToList();
        }
    }
}
