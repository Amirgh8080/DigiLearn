using Common.Application;
using Common.Domain.Utils;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Categories.Edit;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICourseCategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public EditCategoryCommandHandler(ICourseCategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =await _categoryRepository.GetTracking(request.Id);
        if (category == null)
            return OperationResult.NotFound();

        category.Edit(request.Title, request.Slug.ToSlug(), _categoryDomainService);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}
