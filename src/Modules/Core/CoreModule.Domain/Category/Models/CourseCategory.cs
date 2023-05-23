using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Category.DomainServices;

namespace CoreModule.Domain.Category.Models;

public class CourseCategory : BaseEntity
{
    public CourseCategory(string title, string slug, Guid? parentId, ICourseDomainService domainService)
    {
        Guard(title, slug);
        if (domainService.DoesSlugExist(slug))
            throw new InvalidDomainDataException("Slug Already Exists");

        Title = title;
        Slug = slug.ToSlug();
        ParentId = parentId;

    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public Guid? ParentId { get; private set; }

    public void Edig(string title, string slug, ICourseDomainService domainService)
    {
        Guard(title, slug);
        if (Slug != slug)
            if (domainService.DoesSlugExist(slug))  
                throw new InvalidDomainDataException("Slug Already Exists");

        Title = title;
        Slug = slug.ToSlug();
    }


    void Guard(string title, string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug.IsUniCode())
            throw new InvalidDomainDataException("Slug Is Invalid");

    }
}
