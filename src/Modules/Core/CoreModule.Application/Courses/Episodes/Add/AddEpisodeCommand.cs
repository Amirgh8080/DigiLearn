using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.Application.SecurityUtil;
using Common.Domain.Utils;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace CoreModule.Application.Courses.Episodes.Add;

public record AddEpisodeCommand(Guid CourseId, Guid SectiontId, string Title, TimeSpan TimeSpan
    , IFormFile VideoFile, IFormFile? AttachmentFile, bool IsActive, string EnglishTitle) : IBaseCommand;

class AddEpisodeCommandHandler : IBaseCommandHandler<AddEpisodeCommand>
{
    private readonly ICourseRepository _repository;
    private readonly ILocalFileService _localFileService;

    public AddEpisodeCommandHandler(ICourseRepository repository, ILocalFileService localFileService)
    {
        _repository = repository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        string attExt = null;
        if(request.AttachmentFile != null && request.AttachmentFile.IsValidCompressedFile())
        {
            attExt = Path.GetExtension(request.AttachmentFile.FileName);
        }
        string videoExt = Path.GetExtension(request.VideoFile.FileName);
        var episode = course.AddEpisode(request.SectiontId, request.Title, Guid.NewGuid(), request.TimeSpan, videoExt,
            attExt,request.IsActive, request.EnglishTitle.ToSlug());

        
           
        return OperationResult.Success();

    }
    private async Task SaveFiles(AddEpisodeCommand request, Episode episode)
    {
        await _localFileService.SaveFile(request.VideoFile,
                CoreModuleDirectories.CourseEpisode(request.CourseId, Guid.NewGuid()), episode.VideoName);

        if(request.AttachmentFile != null)
        {
            if(request.AttachmentFile.IsValidCompressedFile())
            {
                await _localFileService.SaveFile(request.AttachmentFile,
               CoreModuleDirectories.CourseEpisode(request.CourseId, Guid.NewGuid()), episode.AttachmentName!);
            }
        }

    }
}

public class AddEpisodeCommandValidaor
{
}
