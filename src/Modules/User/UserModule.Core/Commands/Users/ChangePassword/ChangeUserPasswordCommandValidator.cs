using Common.Application.Validation;
using FluentValidation;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommandValidator:AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6).WithMessage("کلمه عبور باید بیشتر از 6 کارکتر باشد");

        RuleFor(r => r.NewPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6).WithMessage("کلمه عبور باید بیشتر از 6 کارکتر باشد");
    }
}