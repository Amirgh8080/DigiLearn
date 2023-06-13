using Common.Domain.Utils;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Categories;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICourseCategoryRepository _repository;

    public CategoryDomainService(ICourseCategoryRepository repository)
    {
        _repository = repository;
    }

    public bool DoesSlugExist(string slug)
    {
        return _repository.Exists(c => c.Slug == slug.ToSlug());
    }
}
