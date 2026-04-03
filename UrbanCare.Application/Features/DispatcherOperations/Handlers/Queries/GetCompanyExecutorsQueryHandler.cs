using MediatR;
using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Application.Features.DispatcherOperations.Queries;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.DispatcherOperations.Handlers.Queries
{
    public class GetCompanyExecutorsQueryHandler : IRequestHandler<GetCompanyExecutorsQuery, List<ExecutorResponseDTO>?>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly GettingDataService _gettingDataService;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public GetCompanyExecutorsQueryHandler(IEmployeeRepository employeeRepository, GettingDataService gettingDataService, IManagementCompanyRepository managementCompanyRepository)
        {
            _employeeRepository = employeeRepository;
            _gettingDataService = gettingDataService;
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<List<ExecutorResponseDTO>?> Handle(GetCompanyExecutorsQuery request, CancellationToken cancellationToken)
        {
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.ManagementCompanyId, cancellationToken);
            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var executors = await _employeeRepository.GetExecutorsByManagementCompanyIdAsync(request.ManagementCompanyId, cancellationToken);
            if (executors == null)
                return null;

            List<ExecutorResponseDTO> executorDTOs = new List<ExecutorResponseDTO>();
            foreach (var executor in executors)
            {
                var employeeDTO = await _gettingDataService.GetEmployeeDataResponseDTOByEmployeeIdAsync(executor.Id, cancellationToken);

                var executorDTO = new ExecutorResponseDTO(
                    employeeDTO,
                    await _employeeRepository.GetExecutorActiveTasksCountAsync(executor.Id, cancellationToken),
                    await _employeeRepository.GetExecutorCompletedTasksCountAsync(executor.Id, cancellationToken));

                executorDTOs.Add(executorDTO);
            }
            return executorDTOs;
        }
    }
}
