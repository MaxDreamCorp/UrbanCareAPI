namespace UrbanCare.Application.DTOs.Responses
{
    public record ExecutorResponseDTO(
        EmployeeDataResponseDTO EmployeeData,
        int ActiveTasksCount,
        int FinishedTasksCount);
}
