using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class DeleteBuildingCommandHandler : IRequestHandler<DeleteBuildingCommand, bool>
    {
        private readonly IBuildingRepository _buildingRepository;

        public DeleteBuildingCommandHandler(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<bool> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
        {
            var building = await _buildingRepository.GetByIdAsync(request.Id, cancellationToken);
            if (building == null)
                throw new Exception("Данного здания не существует");

            await _buildingRepository.RemoveAsync(building, cancellationToken);

            building = await _buildingRepository.GetByIdAsync(request.Id, cancellationToken);
            if (building == null)
                return true;
            return false;
        }
    }
}
