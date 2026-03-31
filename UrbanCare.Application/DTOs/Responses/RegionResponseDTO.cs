namespace UrbanCare.Application.DTOs.Responses
{
    public record RegionResponseDTO(int Id,
        string Name,
        string CommonAddress,
        ManagementCompanyResponseDTO ManagementCompany);
}
