namespace CoreModule.Domain.Categories.DomainServices;

public interface ICategoryDomainService
{
    bool DoesSlugExist(string slug);
}
