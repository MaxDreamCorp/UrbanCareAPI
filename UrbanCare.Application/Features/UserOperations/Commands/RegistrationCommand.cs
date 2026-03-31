using MediatR;
using UrbanCare.Application.DTOs.Requests;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.UserOperations.Commands
{
    public record RegistrationCommand(UserRequestDTO User) : IRequest<RegistrationResponseDTO>;
}
