using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IWallMaterialRepository
    {
        Task<WallMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<WallMaterial>?> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
