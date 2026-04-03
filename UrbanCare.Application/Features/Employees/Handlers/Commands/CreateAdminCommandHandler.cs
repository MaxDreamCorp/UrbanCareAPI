using MediatR;
using UrbanCare.Application.DTOs.Common;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Commands
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, List<ErrorDTO>?>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IQualificationCategoryRepository _qualificationCategoryRepository;
        private readonly IEmployeeStatusRepository _employeeStatusRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public CreateAdminCommandHandler(IEmployeeRepository employeeRepository,
                                         IUserRepository userRepository,
                                         IEmployeePositionRepository employeePositionRepository,
                                         IQualificationCategoryRepository qualificationCategoryRepository,
                                         IEmployeeStatusRepository employeeStatusRepository,
                                         IManagementCompanyRepository managementCompanyRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _employeePositionRepository = employeePositionRepository;
            _qualificationCategoryRepository = qualificationCategoryRepository;
            _employeeStatusRepository = employeeStatusRepository;
            _managementCompanyRepository = managementCompanyRepository;
        }

        public async Task<List<ErrorDTO>?> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserIdAsync(request.EmployeeCreateRequestDTO.UserId, cancellationToken);
            var employeePosition = await _employeePositionRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.EmployeePositionId, cancellationToken);
            var qualPosition = await _qualificationCategoryRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.QualificationCategoryId, cancellationToken);
            var employeeStatus = await _employeeStatusRepository.GetByIdAsync(2, cancellationToken);
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.ManagementCompanyId, cancellationToken);

            if (user == null)
                return new List<ErrorDTO> { new("UserId", "Такого пользователя не существует") };

            if (employeePosition == null)
                return new List<ErrorDTO> { new("EmployeePositionId", "Такой позиции не существует") };

            if (qualPosition == null)
                return new List<ErrorDTO> { new("QualificationCategoryId", "Такой квалификации не существует") };

            if (employeeStatus == null)
                return new List<ErrorDTO> { new("EmployeeStatusId", "Такого статуса не существует") };

            if (managementCompany == null)
                return new List<ErrorDTO> { new("ManagementCompanyId", "Такой управляющей компании не существует") };

            //if (_employeeRepository.GetAdminByManagementCompanyAsync(request.EmployeeCreateRequestDTO.ManagementCompanyId, cancellationToken) != null)
            //    return new List<ErrorDTO> { new("ManagementCompany", "У этой УК уже существует администратор") };

            if (user.RoleId != (int)RolesEnum.Admin)
                return new List<ErrorDTO> { new("UserId", "Пользователь должен иметь роль \"Администратор\"") };

            if (request.EmployeeCreateRequestDTO.EmployeePositionId != (int)RolesEnum.Admin)
                return new List<ErrorDTO> { new("EmployeePosition", "Администратор может быть только на позиции \"Руководитель управляющей компании\"") };

            int newId = await _employeeRepository.GetNextIdAsync(cancellationToken);

            try
            {
                var employeeAdmin = Employee.Create(newId,
                    user,
                    managementCompany,
                    employeePosition,
                    qualPosition,
                    request.EmployeeCreateRequestDTO.EmploymentDate,
                    request.EmployeeCreateRequestDTO.ExpereienceYears,
                    request.EmployeeCreateRequestDTO.Salary,
                    employeeStatus,
                    request.EmployeeCreateRequestDTO.Notes);

                await _employeeRepository.AddAsync(employeeAdmin, cancellationToken);
                return null;
            }
            catch (Exception ex)
            {
                return new() { new ErrorDTO("All", ex.Message) };
            }
        }
    }
}
