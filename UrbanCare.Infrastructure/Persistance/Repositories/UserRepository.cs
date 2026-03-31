using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync())
                return await _context.Users.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone, cancellationToken);
        }

        public async Task<User?> GetUserByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return (await _context.Users.Include(u => u.UserPersonalData).ToListAsync(cancellationToken)).FirstOrDefault(u => u.Id == userId);
        }

        public async Task<List<User>?> GetUsersByRolesAsync(RolesEnum roleEnum, CancellationToken cancellationToken = default)
        {
            var role = await _context.Roles.FindAsync((int)roleEnum, cancellationToken);
            if (role is null) return null;

            return await _context.Users.Where(u => u.RoleId == role.Id).ToListAsync();
        }

        public async Task RemoveAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
