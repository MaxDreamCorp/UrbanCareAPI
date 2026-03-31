using MediatR;
using UrbanCare.Application.Features.UserOperations.Queries;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Features.UserOperations.Handlers.Queries
{
    public class CheckIsEmployeeQueryHandler : IRequestHandler<CheckIsEmployeeQuery, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CheckIsEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(CheckIsEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByUserAsync(request.UserId, cancellationToken);

            return !(employee == null);
        }
    }
}
