using Common.Application;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Application.Courses.Episodes.Accept;

class AcceptEpisodeCommandHandler : IBaseCommandHandler<AcceptEpisodeCommand>
{
    private readonly ICourseRepository _repository;

    public AcceptEpisodeCommandHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AcceptEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();
        if (course.Sections.Any(e => e.Episodes.Any(s => s.Id == request.EpisodeId)))
            return OperationResult.NotFound();

        course.AcceptEpisode(request.EpisodeId);

        await _repository.Save();
        return OperationResult.Success();
    }
}

