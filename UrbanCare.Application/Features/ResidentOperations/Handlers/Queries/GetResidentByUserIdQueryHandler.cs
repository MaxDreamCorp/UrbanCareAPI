using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.ResidentOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.ResidentOperations.Handlers.Queries
{
    public class GetResidentByUserIdQueryHandler : IRequestHandler<GetResidentByUserIdQuery, ResidentResponseDTO?>
    {
        private readonly IResidentRepository _residentRepository;
        private readonly GettingDataService _gettingDataService;

        public GetResidentByUserIdQueryHandler(IResidentRepository residentRepository, GettingDataService gettingDataService)
        {
            _residentRepository = residentRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<ResidentResponseDTO?> Handle(GetResidentByUserIdQuery request, CancellationToken cancellationToken)
        {
            var resident = await _residentRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (resident == null) 
                return null;

            try
            {
                return await _gettingDataService.GetResidentResponseDTOByResidentIdAsync(resident.Id, cancellationToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
