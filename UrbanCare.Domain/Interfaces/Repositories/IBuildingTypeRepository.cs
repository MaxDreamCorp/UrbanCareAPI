using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IBuildingTypeRepository
    {
        Task<BuildingType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<BuildingType>?> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
