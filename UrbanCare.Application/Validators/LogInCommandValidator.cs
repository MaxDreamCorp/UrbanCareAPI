using FluentValidation;
using UrbanCare.Application.Features.UserOperations.Commands;

namespace UrbanCare.Application.Validators
{
    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин обязателен для заполнения");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен для заполнения");

        }
    }
}
