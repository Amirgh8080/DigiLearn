using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace CoreModule.Application.Courses.Create;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(t => t.ImageFile)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.Required)
            .JustImageFile();

        RuleFor(t => t.ImageFile)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .JustValidFile();

        RuleFor(t => t.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);

        RuleFor(t => t.Slug)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.Required);


    }
}
