using FluentValidation;

namespace CoreModule.Application.Categories.Edit;

public class EditCategoryCommandValidator:AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(r => r.Title)
           .NotEmpty()
           .NotNull();
        RuleFor(r => r.Slug)
            .NotEmpty()
            .NotNull();
    }
}
