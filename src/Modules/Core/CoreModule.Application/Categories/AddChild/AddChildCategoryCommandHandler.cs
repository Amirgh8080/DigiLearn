using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Categories.AddChild;

public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public AddChildCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }
    public async Task<OperationResult> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, request.ParentId, _categoryDomainService);

        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}
