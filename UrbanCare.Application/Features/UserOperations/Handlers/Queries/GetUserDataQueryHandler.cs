using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.UserOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.UserOperations.Handlers.Queries
{
    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataResponseDTO?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserDataQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDataResponseDTO?> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return null;

            UserDataResponseDTO response = new(
                user.Id,
                user.Fullname,
                user.Email,
                user.Phone,
                user.RoleId,
                user.UserPersonalData.DateOfBirth);
            return response;
        }
    }
}
