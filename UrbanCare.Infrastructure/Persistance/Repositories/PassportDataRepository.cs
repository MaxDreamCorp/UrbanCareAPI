using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class PassportDataRepository : IPassportDataRepository
    {
        private readonly ApplicationDbContext _context;

        public PassportDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PassportDatum passportData, CancellationToken cancellationToken = default)
        {
            await _context.PassportData.AddAsync(passportData, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public Task<PassportDatum?> GetBySeriaAndNumberAsync(string seria, string number, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.PassportData.AnyAsync())
                return await _context.PassportData.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(PassportDatum passportData, CancellationToken cancellationToken = default)
        {
            _context.PassportData.Remove(passportData);
            await _context.SaveChangesAsync();
        }
    }
}
