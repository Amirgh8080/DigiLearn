using FluentValidation;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(r => r.Family)
            .NotEmpty()
            .NotNull();

    }
}