namespace UrbanCare.Application.DTOs.Responses
{
    public record QualificationCategoryResponseDTO(
        int Id,
        string Name,
        string Code,
        float MinExperienceYears,
        float SalaryCoefficient);
}
