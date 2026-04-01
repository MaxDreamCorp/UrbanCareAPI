namespace UrbanCare.Application.DTOs.Responses
{
    public record OrderExecutorResponseDTO(
        EmployeeDataResponseDTO Employee,
        decimal? WorkPayment);
}
