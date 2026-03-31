namespace UrbanCare.Application.DTOs.Responses
{
    public record OrderCategoryResponseDTO(
        int Id,
        string Category,
        OrderTypeResponseDTO OrderType);
}
