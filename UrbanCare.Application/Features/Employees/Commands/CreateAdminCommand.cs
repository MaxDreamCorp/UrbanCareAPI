using MediatR;
using UrbanCare.Application.DTOs.Common;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Features.Employees.Commands
{
    public record CreateAdminCommand(EmployeeCreateRequestDTO EmployeeCreateRequestDTO) : IRequest<bool>;
}
