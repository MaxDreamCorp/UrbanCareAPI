using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.Employees.Queries
{
    public record GetEmployeeByUserIdQuery(int UserId) : IRequest<EmployeeDataResponseDTO?>;
}
