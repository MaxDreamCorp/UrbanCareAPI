namespace UrbanCare.Application.DTOs.Requests
{
    public record PassportDatumRequestDTO(
        string Seria,
        string Number,
        string Department,
        string DepartmentCode);
}
