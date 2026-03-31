namespace UrbanCare.Application.DTOs.Responses
{
    public record UserDataResponseDTO(
        int Id,
        string Fullname,
        string Email,
        string Phone,
        int RoleId,
        DateOnly DateOfBirth);
}
