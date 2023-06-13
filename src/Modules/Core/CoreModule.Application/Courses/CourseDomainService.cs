using Common.Domain.Utils;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Application.Courses;

public class CourseDomainService : ICourseDomainService
{
    private readonly ICourseRepository _repository;

    public CourseDomainService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public bool DoesSlugExists(string slug)
    {
        return _repository.Exists(c => c.Slug == slug.ToSlug());
    }
}
