using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class UpdateApartmentCommandHandler : IRequestHandler<UpdateApartmentCommand, bool>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBuildingRepository _buildingRepository;

        public UpdateApartmentCommandHandler(IApartmentRepository apartmentRepository, IBuildingRepository buildingRepository)
        {
            _apartmentRepository = apartmentRepository;
            _buildingRepository = buildingRepository;
        }

        public async Task<bool> Handle(UpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            var building = await _buildingRepository.GetByIdAsync(request.BuildingId, cancellationToken);

            if (building == null)
                throw new Exception("Данного здания не существует");

            var apartment = Apartment.Create(
                request.Id,
                request.Number,
                building,
                request.Entrance,
                request.Floor,
                request.RoomCount);

            await _apartmentRepository.UpdateAsync(apartment, cancellationToken);
            return true;
        }
    }
}
