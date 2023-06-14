﻿using Common.Query;

namespace CoreModule.Query.Category._DTOs;

public class CourseCategoryDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }
}
