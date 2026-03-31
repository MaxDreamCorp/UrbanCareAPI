using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.CompanyOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.CompanyOperations.Handlers.Queries
{
    public class GetApartmentsByManagmentCompanyQueryHandler : IRequestHandler<GetApartmentsByManagmentCompanyQuery, List<ApartmentResponseDTO>>
    {
        private readonly IManagementCompanyRepository _managementCompanyRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly GettingDataService _gettingDataService;

        public GetApartmentsByManagmentCompanyQueryHandler(IApartmentRepository apartmentRepository, IManagementCompanyRepository managementCompanyRepository, GettingDataService gettingDataService)
        {
            _apartmentRepository = apartmentRepository;
            _managementCompanyRepository = managementCompanyRepository;
            _gettingDataService = gettingDataService;
        }

        public async Task<List<ApartmentResponseDTO>> Handle(GetApartmentsByManagmentCompanyQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var apartments = await _apartmentRepository.GetByManagementCompanyIdAsync(managementCompany.Id, cancellationToken);

            List<ApartmentResponseDTO> apartmentDTOs = new List<ApartmentResponseDTO>();

            if (apartments == null)
                return apartmentDTOs;

            apartmentDTOs.AddRange(apartments.Select(async a => await _gettingDataService.GetApartmentResponseDTOByApartmentIdAsync(a.Id))
                .Select(a => a.Result));

            return apartmentDTOs;
        }
    }
}
