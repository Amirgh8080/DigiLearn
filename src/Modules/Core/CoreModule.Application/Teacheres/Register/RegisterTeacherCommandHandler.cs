using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Models;
using CoreModule.Domain.Teacher.Repositories;

public class RegisterTeacherCommandHandler : IBaseCommandHandler<RegisterTeacherCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITeacherDomainService _teachereDomainService;
    private readonly ILocalFileService _localFileService;

    public RegisterTeacherCommandHandler(ITeacherRepository teacherRepository, ITeacherDomainService teachereDomainService, ILocalFileService localFileService)
    {
        _teacherRepository = teacherRepository;
        _teachereDomainService = teachereDomainService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName =await _localFileService.SaveFileAndGenerateName(request.CvFile, CoreModuleDirectories.CvFileNames);

        var teacher = new Teacher(request.UserId, request.UserName, cvFileName, _teachereDomainService);

        _teacherRepository.Add(teacher);
        await _teacherRepository.Save();
        return OperationResult.Success();
    }
}
