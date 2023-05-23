using FluentValidation;

namespace CoreModule.Application.Categories.AddChild;

public class AddChildCategoryCommandValidtor:AbstractValidator<AddChildCategoryCommand>
{

    public AddChildCategoryCommandValidtor()
    {
        RuleFor(r => r.Title)
          .NotEmpty()
          .NotNull();
        RuleFor(r => r.Slug)
            .NotEmpty()
            .NotNull();
    }
}