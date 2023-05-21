using FluentValidation;

namespace UserModule.Core.Commands.Notifications.Create;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .NotNull();
        RuleFor(t => t.Text)
            .NotEmpty()
            .NotNull();
    }
}