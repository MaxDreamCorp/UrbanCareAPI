using FluentValidation;
using UrbanCare.Application.Features.UserOperations.Commands;

namespace UrbanCare.Application.Validators
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.User)
                .SetValidator(new UserRequestDtoValidator());
        }
    }
}
