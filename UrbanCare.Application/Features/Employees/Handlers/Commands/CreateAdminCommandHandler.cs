using MediatR;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Commands
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, bool>
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

        public async Task<bool> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserIdAsync(request.EmployeeCreateRequestDTO.UserId, cancellationToken);
            var employeePosition = await _employeePositionRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.EmployeePositionId, cancellationToken);
            var qualPosition = await _qualificationCategoryRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.QualificationCategoryId, cancellationToken);
            var employeeStatus = await _employeeStatusRepository.GetByIdAsync(2, cancellationToken);
            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.ManagementCompanyId, cancellationToken);

            if (user == null)
                throw new Exception("Такого пользователя не существует");

            if (employeePosition == null)
                throw new Exception("Такой позиции не существует");

            if (qualPosition == null)
                throw new Exception("Такой квалификации не существует");

            if (employeeStatus == null)
                throw new Exception("Такого статуса не существует");

            if (managementCompany == null)
                throw new Exception("Такой управляющей компании не существует");

            if (_employeeRepository.GetAdminByManagementCompanyAsync(request.EmployeeCreateRequestDTO.ManagementCompanyId, cancellationToken) != null)
                throw new Exception("У этой УК уже существует администратор");

            if (user.RoleId != (int)RolesEnum.Admin)
                throw new Exception("Пользователь должен иметь роль \"Администратор\"");

            if (request.EmployeeCreateRequestDTO.EmployeePositionId != (int)RolesEnum.Admin)
                throw new Exception("Администратор может быть только на позиции \"Руководитель управляющей компании\"");

            int newId = await _employeeRepository.GetNextIdAsync(cancellationToken);

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
            return true;
        }
    }
}
