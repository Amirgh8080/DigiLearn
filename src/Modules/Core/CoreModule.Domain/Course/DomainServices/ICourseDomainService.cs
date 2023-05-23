namespace CoreModule.Domain.Course.DomainServices;

public interface ICourseDomainService
{
    bool DoesSlugExists(string slug);
}
