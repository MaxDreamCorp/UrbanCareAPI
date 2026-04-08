using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.CompanyOperations.Queries
{
    public record GetCompanyMaterialsQuery(int CompanyId) : IRequest<List<MaterialResponseDTO>>;
}
