using MediatR;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Features.Employees.Commands
{
    public record CreateDispatcherCommand(EmployeeCreateRequestDTO EmployeeCreateRequestDTO) : IRequest<bool>;
}
