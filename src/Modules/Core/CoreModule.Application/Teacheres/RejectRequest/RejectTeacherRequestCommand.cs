using Common.Application;
using CoreModule.Domain.Teacher.Repositories;

namespace CoreModule.Application.Teacheres.RejectRequest;

public record RejectTeacherRequestCommand(Guid TeacherId,string Description):IBaseCommand;

public class RejectTeacherRequestCommandHandler : IBaseCommandHandler<RejectTeacherRequestCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public RejectTeacherRequestCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<OperationResult> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        _teacherRepository.Delete(teacher);
        //Send Event
        await _teacherRepository.Save();

        return OperationResult.Success();
    }
}
