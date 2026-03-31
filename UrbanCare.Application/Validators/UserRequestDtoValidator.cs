using FluentValidation;
using UrbanCare.Application.DTOs.Requests;

namespace UrbanCare.Application.Validators
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDTO>
    {
        public UserRequestDtoValidator()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("ФИО обязательно для заполнения")
                .MinimumLength(2).WithMessage("ФИО должно содержать минимум 2 символа")
                .MaximumLength(255).WithMessage("ФИО не должно превышать 255 символов")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s\-]+$").WithMessage("ФИО может содержать только буквы, пробелы и дефисы");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен для заполнения")
                .EmailAddress().WithMessage("Некорректный формат email")
                .MaximumLength(150).WithMessage("Email не должен превышать 150 символов");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Телефон обязателен для заполнения")
                .Matches(@"^\+?[1-9]\d{10,14}$").WithMessage("Некорректный формат телефона")
                .MaximumLength(20).WithMessage("Телефон не должен превышать 20 символов");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Пароль обязателен")
               .MinimumLength(8).WithMessage("Пароль должен содержать минимум 8 символов")
               .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
               .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
               .Matches(@"\d").WithMessage("Пароль должен содержать хотя бы одну цифру")
               .Matches(@"[!@#$%^&*(),.?""':{}|<>]")
               .WithMessage("Пароль должен содержать хотя бы один специальный символ");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Идентификатор роли должен быть положительным числом")
                .LessThanOrEqualTo(10).WithMessage("Некорректный идентификатор роли");

            RuleFor(x => x.UserPersonalData)
                .NotNull().WithMessage("Персональные данные обязательны")
                .SetValidator(new UserPersonalDataRequestDtoValidator());
        }
    }
}
