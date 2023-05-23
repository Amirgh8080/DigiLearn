using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Enums;

namespace CoreModule.Domain.Course.Models;

public class Course : AggregateRoot
{
    public Course(Guid teacherId, string title, string description, string imageName, string trailerName, int price,
        SeoData seoData, CourseLevel courseLevel, Guid subCategoryId, Guid categoryId, string slug
        ,ICourseDomainService domainService)
    {
        Guard(title, description, imageName, trailerName,slug);
        if (domainService.DoesSlugExists(slug))
            throw new InvalidDomainDataException("Slug Already Exists");

        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        TrailerName = trailerName;
        Price = price;
        LastUpdate = DateTime.Now;
        SeoData = seoData;
        CourseLevel = courseLevel;
        CourseStatus = CourseStatus.StartsSoon;

        Sections = new();
        SubCategoryId = subCategoryId;
        CategoryId = categoryId;
        Slug = slug.ToSlug();
    }

    public Guid TeacherId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SubCategoryId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public string ImageName { get; private set; }
    public string TrailerName { get; private set; }
    public int Price { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public SeoData SeoData { get; private set; }

    public CourseLevel CourseLevel { get; private set; }
    public CourseStatus CourseStatus { get; private set; }


    public List<Section> Sections { get; }


    public void AddSecion(int displayOrder, string title)
    {
        if (Sections.Any(s => s.Title == title))
            throw new InvalidDomainDataException("This Title Already Exists");

        Sections.Add(new Section(title, displayOrder, Id));
    }
    public void EditSecion(Guid sectionId, int displayOrder, string title)
    {
        var section = Sections.SingleOrDefault(s => s.Id == sectionId);
        if (section == null) throw new InvalidDomainDataException("Sections Not Found");

        if (Sections.Any(s => s.Title == title))
            throw new InvalidDomainDataException("This Title Already Exists");

        section.Edit(title, displayOrder);
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.SingleOrDefault(s => s.Id == sectionId);
        if (section == null) throw new InvalidDomainDataException("Sections Not Found");

        Sections.Remove(section);
    }

    public void AddEpisode(Guid sectiontId, string title, Guid token, TimeSpan timeSpan, string videoExtension
        , string? attachmentExtension, bool isActive, string englishTitle)
    {

        var section = Sections.SingleOrDefault(s => s.Id == sectiontId);
        if (section == null) throw new InvalidDomainDataException("Sections Not Found");



        var episodeCount = Sections.Sum(c => c.Episodes.Count());
        var episodeTitle = $"{episodeCount + 1}_{englishTitle}";

        string attName = null;
        if (string.IsNullOrEmpty(attName) == false)
            attName = $"{episodeTitle}.{attachmentExtension}";

        string videoName = $"{episodeTitle}.{videoExtension}";

        if (isActive)
        {
            LastUpdate = DateTime.Now;
            if (CourseStatus == CourseStatus.StartsSoon)
                CourseStatus = CourseStatus.InProgress;
        }

        section.AddEpisode(title, token, timeSpan, videoName, attName, isActive, englishTitle);
    }

    public void AcceptEpisode(Guid episodeId)
    {
        var section = Sections.SingleOrDefault(s => s.Episodes.Any(e => e.Id == episodeId && e.IsActive == false));
        if (section == null)
            throw new InvalidDomainDataException();

        var episode = section.Episodes.First(e => e.Id == episodeId);
        episode.ToggleStatus();
        LastUpdate = DateTime.Now;
    }

    void SetPrice(int price)
    {
        Price = price;
    }

    void Guard(string title, string description, string imageName, string trailerName,string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        NullOrEmptyDomainDataException.CheckString(trailerName, nameof(trailerName));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug.IsUniCode())
            throw new InvalidDomainDataException("Slug is Invalid");
    }
}
