using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.CompanyOperations.Queries
{
    public record GetApartmentsByBuildingQuery(int BuildingId) : IRequest<List<ApartmentResponseDTO>>;
}
