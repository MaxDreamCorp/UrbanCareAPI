namespace UrbanCare.Application.DTOs.Requests
{
    public record UserPersonalDatumRequestDTO(
        PassportDatumRequestDTO PassportData,
        DateOnly DateOfBirth,
        string Snils,
        string Inn);
}
