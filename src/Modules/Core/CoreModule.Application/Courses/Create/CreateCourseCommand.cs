using Common.Application;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Enums;
using CoreModule.Domain.Course.Repository;
using FluentValidation;

namespace CoreModule.Application.Courses.Create;

public class CreateCourseCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageName { get;  set; }
    public string TrailerName { get;  set; }
    public int Price { get;  set; }
    public DateTime LastUpdate { get;  set; }
    public SeoData SeoData { get;  set; }

    public CourseLevel CourseLevel { get;  set; }
    public CourseStatus CourseStatus { get;  set; }
}
public class CreateCourseCommandHandler : IBaseCommandHandler<CreateCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseDomainService _courseDomainService;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseDomainService courseDomainService)
    {
        _courseRepository = courseRepository;
        _courseDomainService = courseDomainService;
    }

    public Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {

    }
}
