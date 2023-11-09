﻿using Common.Domain.Utils;
using CoreModule.Application.Categories.Create;
using CoreModule.Facade.Category;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories
{
    [BindProperties]
    public class AddModel : BaseRazor
    {
        private readonly ICourseCategoryFacade _courseCategoryFacade;

        public AddModel(ICourseCategoryFacade courseCategoryFacade)
        {
            _courseCategoryFacade = courseCategoryFacade;
        }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _courseCategoryFacade.Create(new CreateCategoryCommand()
            {
                Title = Title,
                Slug = Slug.ToSlug()
            } );

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
