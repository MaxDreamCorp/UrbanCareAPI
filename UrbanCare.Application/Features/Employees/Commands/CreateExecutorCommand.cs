using MediatR;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Features.Employees.Commands
{
    public record CreateExecutorCommand(EmployeeCreateRequestDTO EmployeeCreateRequestDTO) : IRequest<bool>;
}
