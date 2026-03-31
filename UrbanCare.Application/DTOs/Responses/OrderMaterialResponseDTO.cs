namespace UrbanCare.Application.DTOs.Responses
{
    public record OrderMaterialResponseDTO(
        int Id,
        int OrderId,
        MaterialResponseDTO Material,
        int Quantity);
}
