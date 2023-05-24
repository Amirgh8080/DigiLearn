﻿using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Application.Courses.Create;

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
            , _courseDomainService)
        {
            Id = id
        };


        _courseRepository.Add(course);
        await _courseRepository.Save(); 
        return OperationResult.Success();
    }
}