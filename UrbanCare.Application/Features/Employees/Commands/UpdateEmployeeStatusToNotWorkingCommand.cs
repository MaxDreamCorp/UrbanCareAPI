using MediatR;

namespace UrbanCare.Application.Features.Employees.Commands
{
    public record UpdateEmployeeStatusToNotWorkingCommand(int UserId) : IRequest;
}
