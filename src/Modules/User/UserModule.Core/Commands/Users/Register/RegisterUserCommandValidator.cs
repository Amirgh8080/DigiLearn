using Common.Application.Validation;
using FluentValidation;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("تلفن همراه"))
            .MinimumLength(11).WithMessage("شماره تلفن همراه معتبر نیست");

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6).WithMessage("کلمه عبور باید بیشتر از 6 کارکتر باشد");

    }
}