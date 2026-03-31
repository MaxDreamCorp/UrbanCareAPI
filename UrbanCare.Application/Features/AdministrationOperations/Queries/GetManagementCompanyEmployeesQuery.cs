using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.AdministrationOperations.Queries
{
    public record GetManagementCompanyEmployeesQuery(int AdminId) : IRequest<List<ReportEmployeeInformationResponseDTO>?>;
}
