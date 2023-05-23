namespace CoreModule.Domain.Category.DomainServices;

public interface ICourseDomainService
{
    bool DoesSlugExist(string slug);
}
