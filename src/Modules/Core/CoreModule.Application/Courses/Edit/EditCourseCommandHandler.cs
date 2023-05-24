using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Application.Courses.Edit;

public class EditCourseCommandHandler : IBaseCommandHandler<EditCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseDomainService _courseDomainService;
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;

    public EditCourseCommandHandler(ICourseRepository courseRepository, ICourseDomainService courseDomainService)
    {
        _courseRepository = courseRepository;
        _courseDomainService = courseDomainService;
    }

    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        var trailerName = course.TrailerName;
        var imageName = course.ImageName;
        if (request.ImageFile.IsImage())
            imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile!, CoreModuleDirectories.CourseImages);

        var oldTrailerName = course.TrailerName;
        var oldImageName = course.ImageName;

        if (request.TrailerFile != null)
        {
            if (request.TrailerFile.IsValidVideoFile() == false)
            {
                return OperationResult.Error("فایل نامعتبر است");
            }
            trailerName = await _ftpFileService.SaveFileAndGenerateName(request.TrailerFile, CoreModuleDirectories.CourseDemo(request.CourseId));
        }
        else
        {
            return OperationResult.Error(ValidationMessages.required("فایل فیلم معرفی"));
        }



        course.Edit(request.Title, request.Description, imageName, trailerName, request.Price
             , request.SeoData, request.CourseLevel, request.CourseStaus, request.SubCategoryId,
             request.CategoryId, request.Slug, _courseDomainService);

        await _courseRepository.Save();
        await DeleteOldFiles(oldImageName, oldTrailerName, request.ImageFile != null, request.TrailerFile != null
            , course);
        return OperationResult.Success();
    }

    async Task DeleteOldFiles(string imageName, string trailerName, bool isImageUploaded, bool isVideoUploaded
        , Course course)
    {
        if (isImageUploaded)
        {
            _localFileService.DeleteFile(CoreModuleDirectories.CourseImages, imageName);
        }
        if (isVideoUploaded)
        {
            _ftpFileService.DeleteFile(CoreModuleDirectories.CourseDemo(course.Id), trailerName);
        }
    }
}
