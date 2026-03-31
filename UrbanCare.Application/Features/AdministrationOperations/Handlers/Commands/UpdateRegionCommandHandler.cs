using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, bool>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public UpdateRegionCommandHandler(IRegionRepository regionRepository, IManagementCompanyRepository managementCompanyRepository)
        {
            _regionRepository = regionRepository;
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<bool> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var region = Region.Create(
                request.Id,
                request.Name,
                request.CommonAddress,
                managementCompany);

            return await _regionRepository.UpdateAsync(region, cancellationToken);
        }
    }
}
