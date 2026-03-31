using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class CreateApartmentCommandHandler : IRequestHandler<CreateApartmentCommand, bool>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBuildingRepository _buildingRepository;

        public CreateApartmentCommandHandler(IApartmentRepository apartmentRepository, IBuildingRepository buildingRepository)
        {
            _apartmentRepository = apartmentRepository;
            _buildingRepository = buildingRepository;
        }

        public async Task<bool> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        {
            var building = await _buildingRepository.GetByIdAsync(request.BuildingId, cancellationToken);

            if (building == null)
                throw new Exception("Данного здания не существует");

            var apartment = Apartment.Create(
                await _apartmentRepository.GetNextIdAsync(cancellationToken),
                request.Number,
                building,
                request.Entrance,
                request.Floor,
                request.RoomCount);

            await _apartmentRepository.AddAsync(apartment, cancellationToken);
            return true;
        }
    }
}
