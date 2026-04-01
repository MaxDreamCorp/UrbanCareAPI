namespace UrbanCare.Application.DTOs.Responses
{
    public record EmployeeDataResponseDTO(int Id,
        UserDataResponseDTO UserData,
        ManagementCompanyResponseDTO ManagementCompany,
        EmployeePositionResponseDTO EmployeePosition,
        EmployeeStatusResponseDTO EmployeeStatus,
        QualificationCategoryResponseDTO QualificationCategory,
        DateOnly EmploymentDate,
        int ExpereienceYears,
        int Salary,
        string? Notes);
}
