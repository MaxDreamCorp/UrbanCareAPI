using MediatR;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.AdministrationOperations.Handlers.Commands
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand, bool>
    {
        private readonly IApartmentRepository _apartmentRepository;

        public DeleteApartmentCommandHandler(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<bool> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (apartment == null)
                throw new Exception("Данной квартиры не существует");

            await _apartmentRepository.RemoveAsync(apartment, cancellationToken);

            apartment = await _apartmentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (apartment == null)
                return true;
            return false;
        }
    }
}
