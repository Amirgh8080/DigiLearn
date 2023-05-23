using Common.Application;
using CoreModule.Domain.Teacher.Repositories;

namespace CoreModule.Application.Teacheres.AcceptRequest;

public class AcceptTeacherRequestCommandHandler : IBaseCommandHandler<AcceptTeacherRequestCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public AcceptTeacherRequestCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<OperationResult> Handle(AcceptTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();
        
        teacher.AcceptRequest();
        await _teacherRepository.Save();

        return OperationResult.Success();
    }
}
