using UrbanCare.Domain.Entities;

namespace UrbanCare.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
