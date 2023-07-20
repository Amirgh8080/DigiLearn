using CoreModule.Application.Courses.Sections.AddSection;
using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;

public class AddModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public AddModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public string Title { get; set; }
    public int DisplayOrder { get; set; }
    public Guid CourseId { get; set; }

    public void OnGet(Guid courseId)
    {
         CourseId= courseId;
    }

    public async Task<IActionResult> OnPost()
    {
        var result =await _courseFacade.AddSection(new AddCourseSectionCommand(CourseId,Title,DisplayOrder));

        return RedirectAndShowAlert(result,RedirectToPage("/Index"));
    }
}
