using Common.Domain;
using Common.Domain.Exceptions;

namespace CoreModule.Domain.Course.Models;

public class Section : BaseEntity
{
    public Section(string title, int displayOrder, Guid courseId)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));

        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;

        Episodes = new List<Episode>();
    }

    public Guid CourseId { get; private set; }
    public string Title { get; private set; }
    public int DisplayOrder { get; private set; }

    public List<Episode> Episodes { get; private set; }

    public void Edit(string title, int displayOrder)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));

        Title = title;
        DisplayOrder = displayOrder;
    }
    public Episode AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName,
        bool isActive, string englishTitle)
    {
        var episdoe = new Episode(title, token, timeSpan, videoName, attachmentName, isActive, Id, englishTitle);
        Episodes.Add(episdoe);
        return episdoe;
    }
}
