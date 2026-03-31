using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand, bool>
    {
        private readonly IRegionRepository _regionRepository;

        public DeleteRegionCommandHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<bool> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (region == null)
                throw new Exception("Данного региона не существует");

            await _regionRepository.RemoveAsync(region, cancellationToken);

            region = await _regionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (region == null)
                return true;
            return false;
        }
    }
}
