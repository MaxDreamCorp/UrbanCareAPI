using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class UserPersonalDataRepository : IUserPersonalDataRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPersonalDataRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(UserPersonalDatum userPersonalData, CancellationToken cancellationToken = default)
        {
            await _context.UserPersonalData.AddAsync(userPersonalData, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.UserPersonalData.AnyAsync())
                return await _context.UserPersonalData.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(UserPersonalDatum userPersonalData, CancellationToken cancellationToken = default)
        {
            _context.UserPersonalData.Remove(userPersonalData);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
