using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Domain.Entities;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.Employees.Handlers.Commands
{
    public class CreateDispatcherCommandHandler : IRequestHandler<CreateDispatcherCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IQualificationCategoryRepository _qualificationCategoryRepository;
        private readonly IEmployeeStatusRepository _employeeStatusRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;

        public CreateDispatcherCommandHandler(IEmployeeRepository employeeRepository,
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

        public async Task<bool> Handle(CreateDispatcherCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserIdAsync(request.EmployeeCreateRequestDTO.UserId, cancellationToken);
            if (user == null)
                throw new Exception("Такого пользователя не существует");

            var existingEmployee = await _employeeRepository.GetByUserAsync(request.EmployeeCreateRequestDTO.UserId, cancellationToken);
            if (existingEmployee != null)
                throw new Exception("Пользователь уже является сотрудником");

            var employeePosition = await _employeePositionRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.EmployeePositionId, cancellationToken);
            if (employeePosition == null)
                throw new Exception("Такой позиции не существует");

            var qualPosition = await _qualificationCategoryRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.QualificationCategoryId, cancellationToken);
            if (qualPosition == null)
                throw new Exception("Такой квалификации не существует");

            var employeeStatus = await _employeeStatusRepository.GetByIdAsync(2, cancellationToken);

            if (employeeStatus == null)
                throw new Exception("Такого статуса не существует");

            var managementCompany = await _managementCompanyRepository.GetByIdAsync(request.EmployeeCreateRequestDTO.ManagementCompanyId, cancellationToken);
            if (managementCompany == null)
                throw new Exception("Такой управляющей компании не существует");

            if (user.RoleId != (int)RolesEnum.Dispatcher)
                throw new Exception("Пользователь должен иметь роль \"Диспетчер\"");

            if (employeePosition.Id != 3)
                throw new Exception("Неверная позиция для диспетчера");

            int newId = await _employeeRepository.GetNextIdAsync(cancellationToken);

            var dispatcher = Employee.Create(
                newId,
                user,
                managementCompany,
                employeePosition,
                qualPosition,
                request.EmployeeCreateRequestDTO.EmploymentDate,
                request.EmployeeCreateRequestDTO.ExpereienceYears,
                request.EmployeeCreateRequestDTO.Salary,
                employeeStatus,
                request.EmployeeCreateRequestDTO.Notes);

            await _employeeRepository.AddAsync(dispatcher, cancellationToken);
            return true;
        }
    }
}
