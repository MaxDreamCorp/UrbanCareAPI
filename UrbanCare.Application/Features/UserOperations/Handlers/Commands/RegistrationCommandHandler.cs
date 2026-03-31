using FluentValidation;
using MediatR;
using UrbanCare.Application.DTOs.Common;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.UserOperations.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;
using UrbanCare.Domain.Interfaces.Security;

namespace UrbanCare.Application.Features.UserOperations.Handlers.Commands
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPersonalDataRepository _userPersonalDataRepository;
        private readonly IPassportDataRepository _passportDataRepository;
        private readonly IHasher _hasher;
        private readonly IValidator<RegistrationCommand> _validator;

        public RegistrationCommandHandler(
            IUserRepository userRepository,
            IHasher hasher,
            IValidator<RegistrationCommand> validator,
            IUserPersonalDataRepository userPersonalDataRepository,
            IPassportDataRepository passportDataRepository)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _validator = validator;
            _userPersonalDataRepository = userPersonalDataRepository;
            _passportDataRepository = passportDataRepository;
        }

        public async Task<RegistrationResponseDTO> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new RegistrationResponseDTO(-1, validationResult.Errors.Select(
                    e => new ErrorDTO(e.PropertyName, e.ErrorMessage))
                    .ToList());

            var oldUserByPhone = await _userRepository.GetUserByPhoneAsync(request.User.Phone, cancellationToken);
            var oldUserByEmail = await _userRepository.GetUserByEmailAsync(request.User.Email, cancellationToken);

            if (oldUserByPhone != null || oldUserByEmail != null)
                return new RegistrationResponseDTO(-1, new() {
                    new ErrorDTO("Email", "Пользователь с такими данными уже существует")});

            var hashedPassword = _hasher.Hash(request.User.Password);

            var userDTO = request.User;
            var userPersonalDataDTO = userDTO.UserPersonalData;
            var passportDataDTO = userPersonalDataDTO.PassportData;

            PassportDatum newPassportData = PassportDatum.Create(
                await _passportDataRepository.GetNextIdAsync(cancellationToken),
                passportDataDTO.Seria,
                passportDataDTO.Number,
                passportDataDTO.Department,
                passportDataDTO.DepartmentCode);

            UserPersonalDatum newUserPersonalData = UserPersonalDatum.Create(
                await _userPersonalDataRepository.GetNextIdAsync(cancellationToken),
                userPersonalDataDTO.DateOfBirth,
                userPersonalDataDTO.Snils,
                userPersonalDataDTO.Inn,
                newPassportData);

            User newUser = User.Create(
                await _userRepository.GetNextIdAsync(cancellationToken),
                userDTO.Fullname,
                userDTO.Email,
                userDTO.Phone,
                hashedPassword.hash,
                hashedPassword.salt,
                userDTO.RoleId,
                newUserPersonalData);

            await _userRepository.AddAsync(newUser, cancellationToken);

            return new(newUser.Id, null);
        }
    }
}
