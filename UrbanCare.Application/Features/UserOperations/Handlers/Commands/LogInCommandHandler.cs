using FluentValidation;
using MediatR;
using UrbanCare.Application.DTOs.Common;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.UserOperations.Commands;
using UrbanCare.Application.Interfaces;
using UrbanCare.Domain.Interfaces.Repositories;
using UrbanCare.Domain.Interfaces.Security;

namespace UrbanCare.Application.Features.UserOperations.Handlers.Commands
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, LogInResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<LogInCommand> _validator;

        public LogInCommandHandler(IUserRepository userRepository,
                                   IHasher hasher,
                                   IJwtProvider jwtProvider,
                                   IValidator<LogInCommand> validator)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        public async Task<LogInResponseDTO> Handle(LogInCommand request,
                                                   CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new LogInResponseDTO(null, -1, validationResult.Errors.Select(
                    e => new ErrorDTO(e.PropertyName, e.ErrorMessage))
                    .ToList());

            var user = await _userRepository.GetUserByEmailAsync(request.Login, cancellationToken);

            if (user is null)
                user = await _userRepository.GetUserByPhoneAsync(request.Login, cancellationToken);

            if (user is null)
                return new LogInResponseDTO(null, -1, new List<ErrorDTO>
                {
                    new ErrorDTO("Login", "Пользователя с таким логином не существует")
                });

            if (!_hasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
                return new LogInResponseDTO(null, -1, new List<ErrorDTO>
                {
                    new ErrorDTO("Password", "Неверный пароль")
                });

            var token = _jwtProvider.GenerateToken(user);

            return new LogInResponseDTO(token, user.RoleId, null);
        }
    }
}
