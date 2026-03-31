using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.AdministrationOperations.Queries
{
    public record GetAdminManagementCompanyQuery(int employeeId) : IRequest<ManagementCompanyResponseDTO?>;
}
