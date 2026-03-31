using UrbanCare.Application.DTOs.Common;

namespace UrbanCare.Application.DTOs.Responses
{
    public record LogInResponseDTO(string? Token, int RoleId, List<ErrorDTO>? Errors);
}
