using Common.Application.Validation.FluentValidations;
using FluentValidation;

public class RegisterTeacherCommandValidator:AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(r => r.UserName)
          .NotEmpty()
          .NotNull();

        RuleFor(r => r.CvFile)
            .NotEmpty()
            .NotNull()
            .JustValidFile();
    }
}
