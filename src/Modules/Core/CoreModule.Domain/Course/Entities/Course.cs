using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Course.Enums;

namespace CoreModule.Domain.Course.Entities;

public class Course : BaseEntity
{
    public Course(Guid teacherId, string title, string description, string imageName, string trailerName, int price,
        SeoData seoData, CourseLevel courseLevel)
    {
        Guard(title, description, imageName, trailerName);

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
    }

    public Guid TeacherId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ImageName { get; private set; }
    public string TrailerName { get; private set; }
    public int Price { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public SeoData SeoData { get; private set; }

    public CourseLevel CourseLevel { get; private set; }
    public CourseStatus CourseStatus { get; private set; }


    public List<Section> Sections { get; }


    void AddSecion(int displayOrder , string title)
    {
        if (Sections.Any(s => s.Title == title))
            throw new InvalidDomainDataException("This Title Already Exists");

        Sections.Add(new Section(title, displayOrder));
    }

    void RemoveSection(Guid SectionId)
    {
        var section = Sections.SingleOrDefault(s => s.Id == SectionId);

        if (section == null) throw new InvalidDomainDataException("Sections Not Found");

        Sections.Remove(section);
    }

    void SetPrice(int price)
    {
        Price = price;
    }

    void Guard(string title, string description, string imageName, string trailerName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        NullOrEmptyDomainDataException.CheckString(trailerName, nameof(trailerName));
    }
}
