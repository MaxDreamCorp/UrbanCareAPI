using MediatR;
using UrbanCare.Application.Features.ResidentOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ResidentOperations.Handlers.Commands
{
    public class CreateResidentCommandHandler : IRequestHandler<CreateResidentCommand, bool>
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public CreateResidentCommandHandler(IResidentRepository residentRepository, IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _residentRepository = residentRepository;
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<bool> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserIdAsync(request.UserId, cancellationToken);
            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

            if (user == null)
                throw new Exception("Данного пользователя не существует");
            if (apartment == null)
                throw new Exception("Данной квартиры не существует");

            var newResident = Resident.Create(
                await _residentRepository.GetNextIdAsync(cancellationToken),
                user,
                apartment,
                request.MovingIntoDate,
                null,
                true);

            await _residentRepository.AddAsync(newResident, cancellationToken);
            return true;
        }
    }
}
