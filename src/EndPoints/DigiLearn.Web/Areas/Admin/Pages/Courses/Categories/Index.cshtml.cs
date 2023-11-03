using CoreModule.Facade.Category;
using CoreModule.Query.Category._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories
{
    public class IndexModel : BaseRazor
    {
        ICourseCategoryFacade _courseFacade;

        public IndexModel(ICourseCategoryFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        public List<CourseCategoryDto> Categories { get; set; }

        public async Task OnGet()
        {
            Categories = await _courseFacade.GetMainCategories();
        }
    }
}
