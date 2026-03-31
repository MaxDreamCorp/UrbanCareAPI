using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.OrderOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.OrderOperations.Handlers.Queries
{
    public class GetPrioritiesQueryHandler : IRequestHandler<GetPrioritiesQuery, List<PriorityResponseDTO>>
    {
        private readonly IPriorityRepository _priorityRepository;

        public GetPrioritiesQueryHandler(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<List<PriorityResponseDTO>> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _priorityRepository.GetAllAsync(cancellationToken);

            return priorities.Select(p => new PriorityResponseDTO(p.Id, p.Priority1)).ToList();
        }
    }
}
