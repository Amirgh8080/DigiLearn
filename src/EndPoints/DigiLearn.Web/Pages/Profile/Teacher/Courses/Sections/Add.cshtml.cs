using CoreModule.Application.Courses.Sections.AddSection;
using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public AddModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public string Title { get; set; }
    public int DisplayOrder { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result =await _courseFacade.AddSection(new AddCourseSectionCommand(courseId,Title,DisplayOrder));

        return RedirectAndShowAlert(result,RedirectToPage("/Index",new  { courseId }));
    }
}
