using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Application.Courses.Sections.AddSection;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.GetByFilter;
using CoreModule.Query.Course.GetById;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<OperationResult> AddSection(AddCourseSectionCommand command);
    Task<OperationResult> AddEpisode(AddEpisodeCommand command);

    Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams param);
    Task<CourseDto?> GetCourseById(Guid courseId);
}
public class CourseFacade : ICourseFacade
{
    private readonly IMediator _mediator;

    public CourseFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddEpisode(AddEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams param)
    {
        return await _mediator.Send(new GetCoursesByFilterQuery(param));
    }

    public async Task<CourseDto?> GetCourseById(Guid courseId)
    {
        return await _mediator.Send(new GetCourseByIdQuery(courseId));
    }
}
