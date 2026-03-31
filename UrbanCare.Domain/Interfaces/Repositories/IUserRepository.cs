using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task RemoveAsync(User user, CancellationToken cancellationToken = default);
        Task<List<User>?> GetUsersByRolesAsync(RolesEnum roleEnum, CancellationToken cancellationToken = default);
        Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
        Task<User?> GetUserByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<User?> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
