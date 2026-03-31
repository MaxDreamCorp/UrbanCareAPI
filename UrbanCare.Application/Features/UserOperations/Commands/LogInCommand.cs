using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.UserOperations.Commands
{
    public record LogInCommand(string Login, string Password) : IRequest<LogInResponseDTO>;
}
