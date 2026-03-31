using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IFloorMaterialRepository
    {
        Task<FloorMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<FloorMaterial>?> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
