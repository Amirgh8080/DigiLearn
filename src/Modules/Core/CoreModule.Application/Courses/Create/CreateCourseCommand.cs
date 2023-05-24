using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Enums;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Create;

public class CreateCourseCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public IFormFile TrailerFile { get; set; }
    public int Price { get; set; }
    public SeoData SeoData { get; set; }

    public CourseLevel CourseLevel { get; set; }
}
class CreateCourseCommandHandler : IBaseCommandHandler<CreateCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseDomainService _courseDomainService;
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseDomainService courseDomainService)
    {
        _courseRepository = courseRepository;
        _courseDomainService = courseDomainService;
    }

    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        Guid id = Guid.NewGuid();
        var trailerName = "";
        var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImages);
        if (request.TrailerFile != null)
        {
            if (request.TrailerFile.IsValidVideoFile() == false)
            {
                return OperationResult.Error("فایل نامعتبر است");
            }
           trailerName = await _ftpFileService.SaveFileAndGenerateName(request.TrailerFile, CoreModuleDirectories.CourseDemo(id));
        }
        else
        {
            return OperationResult.Error(ValidationMessages.required("فایل فیلم معرفی"));
        }



        var course = new Course(request.TeacherId, request.Title, request.Description, imageName, trailerName, request.Price
            , request.SeoData, request.CourseLevel, request.SubCategoryId, request.CategoryId, request.Slug
            , _courseDomainService);


        return OperationResult.Success();
    }
}
