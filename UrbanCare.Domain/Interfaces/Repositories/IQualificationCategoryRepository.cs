using UrbanCare.Domain.Entities;

namespace UrbanCare.Domain.Interfaces.Repositories
{
    public interface IQualificationCategoryRepository
    {
        Task<QualificationCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<QualificationCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
