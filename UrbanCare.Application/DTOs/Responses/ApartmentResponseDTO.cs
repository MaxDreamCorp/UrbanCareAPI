namespace UrbanCare.Application.DTOs.Responses
{
    public record ApartmentResponseDTO(
        int Id,
        int Number,
        BuildingResponseDTO Building,
        int? Entrance,
        int Floor,
        int RoomCount,
        bool IsFree);
}
