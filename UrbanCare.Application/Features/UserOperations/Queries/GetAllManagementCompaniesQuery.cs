using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.UserOperations.Queries
{
    public record GetAllManagementCompaniesQuery() : IRequest<List<ManagementCompanyResponseDTO>>;
}
