using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Infrastructure.Persistance.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Resident resident, CancellationToken cancellationToken = default)
        {
            await _context.Residents.AddAsync(resident, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Resident?> GetCurrentByApartmentIdAsync(int apartmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Residents.FirstOrDefaultAsync(r => r.ApartmentId == apartmentId &&
                r.IsLivingBool, cancellationToken);
        }

        public async Task<Resident?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Residents
                .Include(r => r.User)
                    .ThenInclude(u => u.UserPersonalData)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Residents.AnyAsync())
                return await _context.Residents.MaxAsync(x => x.Id) + 1;
            return 1;
        }

        public async Task RemoveAsync(Resident resident, CancellationToken cancellationToken = default)
        {
            _context.Residents.Remove(resident);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Resident resident, CancellationToken cancellationToken = default)
        {
            var oldResident = await GetByIdAsync(resident.Id, cancellationToken);
            if (oldResident == null) return false;

            oldResident.UserId = resident.UserId;
            oldResident.ApartmentId = resident.ApartmentId;
            oldResident.MovingIntoDate = resident.MovingIntoDate;
            oldResident.MovingOutDate = resident.MovingOutDate;
            oldResident.IsLivingBool = resident.IsLivingBool;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Resident?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Residents
                .Include(r => r.User)
                    .ThenInclude(u => u.UserPersonalData)
                .FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);
        }
    }
}
