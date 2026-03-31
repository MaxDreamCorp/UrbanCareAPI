namespace UrbanCare.Application.DTOs.Requests
{
    public record UserRequestDTO(
        string Fullname,
        string Email,
        string Phone,
        string Password,
        int RoleId,
        UserPersonalDatumRequestDTO UserPersonalData);
}
