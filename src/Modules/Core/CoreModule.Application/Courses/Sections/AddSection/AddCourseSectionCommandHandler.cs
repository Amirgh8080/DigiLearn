
using Common.Application;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Application.Courses.Sections.AddSection;

class AddCourseSectionCommandHandler : IBaseCommandHandler<AddCourseSectionCommand>
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseSectionCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<OperationResult> Handle(AddCourseSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        course.AddSecion(request.DisplayOrder, request.Title);

        await _courseRepository.Save();

        return OperationResult.Success();
    }
}
