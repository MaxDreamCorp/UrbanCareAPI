using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class UpdateBuildingCommandHandler : IRequestHandler<UpdateBuildingCommand, bool>
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IBuildingTypeRepository _buildingTypeRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IFloorMaterialRepository _floorMaterialRepository;
        private readonly IWallMaterialRepository _wallMaterialRepository;

        public UpdateBuildingCommandHandler(IBuildingRepository buildingRepository,
                                            IBuildingTypeRepository buildingTypeRepository,
                                            IRegionRepository regionRepository,
                                            IFloorMaterialRepository floorMaterialRepository,
                                            IWallMaterialRepository wallMaterialRepository)
        {
            _buildingRepository = buildingRepository;
            _buildingTypeRepository = buildingTypeRepository;
            _regionRepository = regionRepository;
            _floorMaterialRepository = floorMaterialRepository;
            _wallMaterialRepository = wallMaterialRepository;
        }

        public async Task<bool> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetByIdAsync(request.RegionId, cancellationToken);
            var buildingType = await _buildingTypeRepository.GetByIdAsync(request.BuildingTypeId, cancellationToken);
            var floorMaterial = await _floorMaterialRepository.GetByIdAsync(request.FloorMaterialId, cancellationToken);
            var wallMaterial = await _wallMaterialRepository.GetByIdAsync(request.WallMaterialId, cancellationToken);

            if (region == null)
                throw new Exception("Данной УК не существует");
            if (buildingType == null)
                throw new Exception("Данного типа здания не существует");
            if (floorMaterial == null)
                throw new Exception("Данного материала перекрытий не существует");
            if (wallMaterial == null)
                throw new Exception("Данного материала стен не существует");

            var building = Building.Create(
                 request.Id,
                 request.Number,
                 request.Address,
                 region,
                 buildingType,
                 request.YearBuit,
                 request.FloorCount,
                 floorMaterial,
                 wallMaterial);

            await _buildingRepository.UpdateAsync(building, cancellationToken);
            return true;
        }
    }
}