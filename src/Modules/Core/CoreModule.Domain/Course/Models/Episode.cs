using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using System;

namespace CoreModule.Domain.Course.Models;

public class Episode : BaseEntity
{
    public Episode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName,
        bool isActive, Guid sectionId, string englishTitle)
    {
        Guard(videoName, englishTitle, title);

        Title = title;
        Token = token;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        SectionId = sectionId;
        EnglishTitle = englishTitle;
    }

    public string Title { get; private set; }
    public string EnglishTitle { get; private set; }
    public Guid Token { get; private set; }
    public Guid SectionId { get; private set; }
    public TimeSpan TimeSpan { get; private set; }
    public string VideoName { get; private set; }
    public string? AttachmentName { get; private set; }
    public bool IsActive { get; private set; }


    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }

    void Guard(string videoName, string englishTitle, string title)
    {
        NullOrEmptyDomainDataException.CheckString(videoName, nameof(videoName));
        NullOrEmptyDomainDataException.CheckString(englishTitle, nameof(englishTitle));
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));

        if (englishTitle.IsUniCode())
        {
            throw new InvalidDomainDataException("Invalid English Title");
        }
    }
}
