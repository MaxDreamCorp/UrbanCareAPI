namespace UrbanCare.Application.DTOs.Responses
{
    public record MaterialResponseDTO(
        int Id,
        string Name,
        string Unit,
        decimal Price);
}
