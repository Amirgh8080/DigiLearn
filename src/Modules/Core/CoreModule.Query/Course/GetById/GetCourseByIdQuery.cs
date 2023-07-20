using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetById;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto?>;

class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly QueryContext _context;

    public GetCourseByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .SingleOrDefaultAsync(c => c.Id == request.Id,cancellationToken : cancellationToken);
        if (course == null)
            return null;
        return new CourseDto()
        {
            Id = course.Id,
            Title = course.Title,
            CategoryId = course.CategoryId,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            CreationDate = course.CreationDate,
            Description = course.Description,
            ImageName = course.ImageName,
            LastUpdate = course.LastUpdate,
            Price = course.Price,
            Sections = course.Sections.Select(s => new CourseSectionDto()
            {
                Id = s.Id,
                Title = s.Title,
                CourseId = s.CourseId,
                CreationDate = s.CreationDate,
                DisplayOrder = s.DisplayOrder,
                Episodes = s.Episodes.Select(e => new EpisodeDto()
                {
                    CreationDate = e.CreationDate,
                    AttachmentName = e.AttachmentName,
                    EnglishTitle = e.EnglishTitle,
                    Id = e.Id,
                    IsActive = e.IsActive,
                    SectionId = e.SectionId,
                    TimeSpan = e.TimeSpan,
                    Title = e.Title,
                    Token = e.Token,
                    VideoName = e.VideoName
                }).ToList()
            }).ToList()
        };
    }
}
