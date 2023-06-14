using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.GetByFilter;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);

    Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams param);
}
public class CourseFacade : ICourseFacade
{
    private readonly IMediator _mediator;

    public CourseFacade(IMediator mediator)
    {
        _mediator = mediator;
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
}
