using FluentValidation;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Validators
{
    public class PassportDataRequestDtoValidator : AbstractValidator<PassportDatumRequestDTO>
    {
        public PassportDataRequestDtoValidator()
        {
            RuleFor(x => x.Seria)
            .NotEmpty().WithMessage("Серия паспорта обязательна")
            .Length(4).WithMessage("Серия паспорта должна содержать 4 цифры")
            .Matches(@"^\d{4}$").WithMessage("Серия паспорта может содержать только цифры");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Номер паспорта обязателен")
                .Length(6).WithMessage("Номер паспорта должен содержать 6 цифр")
                .Matches(@"^\d{6}$").WithMessage("Номер паспорта может содержать только цифры");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Кем выдан паспорт - обязательное поле")
                .MaximumLength(500).WithMessage("Наименование отдела не должно превышать 500 символов");

            RuleFor(x => x.DepartmentCode)
                .NotEmpty().WithMessage("Код подразделения обязателен")
                .Matches(@"^\d{3}-\d{3}$").WithMessage("Код подразделения должен быть в формате: XXX-XXX");

        }
    }
}
