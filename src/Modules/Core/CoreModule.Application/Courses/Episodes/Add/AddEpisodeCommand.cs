using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Course.Repository;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Episodes.Add;

public record AddEpisodeCommand(Guid CourseId, Guid sectiontId, string title, Guid token, TimeSpan timeSpan
    , IFormFile video, IFormFile? attachment, bool isActive, string englishTitle) : IBaseCommand;

class AddEpisodeCommandHandler : IBaseCommandHandler<AddEpisodeCommand>
{
    private readonly ICourseRepository _repository;
    private readonly IFtpFileService _ftpFileService;

    public AddEpisodeCommandHandler(ICourseRepository repository, IFtpFileService ftpFileService)
    {
        _repository = repository;
        _ftpFileService = ftpFileService;
    }

    public async Task<OperationResult> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        var fileName = await _ftpFileService.SaveFileAndGenerateName(request.video, CoreModuleDirectories.CvFileNames);
        // course.AddEpisode(request.sectiontId,request.title.SanitizeText(),request.token,request.timeSpan,)
        return OperationResult.Success();
        
    }
}
public class AddEpisodeCommandValidaor
{
}
