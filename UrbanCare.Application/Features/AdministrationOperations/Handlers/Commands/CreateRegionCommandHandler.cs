using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, bool>
    {
        private readonly IManagementCompanyRepository _managementCompanyRepository;
        private readonly IRegionRepository _regionRepository;

        public CreateRegionCommandHandler(IManagementCompanyRepository managementCompanyRepository, IRegionRepository regionRepository)
        {
            _managementCompanyRepository = managementCompanyRepository;
            _regionRepository = regionRepository;
        }

        public async Task<bool> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var newRegion = Region.Create(
                await _regionRepository.GetNextIdAsync(cancellationToken),
                request.Name,
                request.CommonAddress,
                managementCompany);

            await _regionRepository.AddAsync(newRegion, cancellationToken);
            return true;
        }
    }
}
