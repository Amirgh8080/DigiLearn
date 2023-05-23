using FluentValidation;

namespace CoreModule.Application.Categories.Create;

public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .NotNull();
        RuleFor(r => r.Slug)
            .NotEmpty()
            .NotNull();
    }

}
