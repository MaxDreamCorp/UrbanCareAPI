using FluentValidation;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Validators
{
    public class UserPersonalDataRequestDtoValidator : AbstractValidator<UserPersonalDatumRequestDTO>
    {
        public UserPersonalDataRequestDtoValidator()
        {
            RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Дата рождения обязательна")
            .Must(BeValidDateOfBirth).WithMessage("Дата рождения должна быть в диапазоне от {0} до {1}")
            .WithMessage("Дата рождения не может быть в будущем");

            RuleFor(x => x.Snils)
                .NotEmpty().WithMessage("СНИЛС обязателен")
                .Matches(@"^\d{3}-\d{3}-\d{3}\s\d{2}$")
                .WithMessage("СНИЛС должен быть в формате: XXX-XXX-XXX XX");

            RuleFor(x => x.Inn)
                .NotEmpty().WithMessage("ИНН обязателен")
                .Matches(@"^\d{12}$").WithMessage("ИНН должен состоять из 12 цифр");

            RuleFor(x => x.PassportData)
                .NotNull().WithMessage("Паспортные данные обязательны")
                .SetValidator(new PassportDataRequestDtoValidator());
        }

        private bool BeValidDateOfBirth(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var minDate = today.AddYears(-150);
            return dateOfBirth < today && dateOfBirth > minDate;
        }
    }
}
