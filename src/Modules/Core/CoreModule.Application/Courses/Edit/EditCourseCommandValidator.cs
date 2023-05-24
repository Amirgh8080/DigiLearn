using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace CoreModule.Application.Courses.Edit;

public class EditCourseCommandValidator :AbstractValidator<EditCourseCommand>
{
    public EditCourseCommandValidator()
    {
        RuleFor(t => t.ImageFile)
           .JustImageFile();

        RuleFor(t => t.TrailerFile)
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
