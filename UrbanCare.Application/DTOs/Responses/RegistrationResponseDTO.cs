using UrbanCare.Application.DTOs.Common;

namespace UrbanCare.Application.DTOs.Responses
{
    public record RegistrationResponseDTO(int UserId, List<ErrorDTO>? Errors);
}
