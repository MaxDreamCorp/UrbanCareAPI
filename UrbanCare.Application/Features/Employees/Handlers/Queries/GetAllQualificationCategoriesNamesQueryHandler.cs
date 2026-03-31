using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.Employees.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Queries
{
    public class GetAllQualificationCategoriesNamesQueryHandler : IRequestHandler<GetAllQualificationCategoriesNamesQuery, List<QualificationCategoriesNamesResponseDTO>>
    {
        private readonly IQualificationCategoryRepository _qualificationCategoryRepository;

        public GetAllQualificationCategoriesNamesQueryHandler(IQualificationCategoryRepository qualificationCategoryRepository)
        {
            _qualificationCategoryRepository = qualificationCategoryRepository;
        }

        public async Task<List<QualificationCategoriesNamesResponseDTO>> Handle(GetAllQualificationCategoriesNamesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _qualificationCategoryRepository.GetAllAsync(cancellationToken);
            return categories.Select(c => new QualificationCategoriesNamesResponseDTO(c.Id, c.Name)).ToList();
        }
    }
}
