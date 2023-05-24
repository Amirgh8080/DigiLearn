﻿using Common.Application;
using Common.Domain.ValueObjects;
using CoreModule.Application.Courses.Create;
using CoreModule.Domain.Course.Enums;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Courses.Edit;

public class EditCourseCommand : IBaseCommand
{
    public Guid CourseId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IFormFile? TrailerFile { get; set; }
    public int Price { get; set; }
    public SeoData SeoData { get; set; }

    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStaus { get; set; }
}
