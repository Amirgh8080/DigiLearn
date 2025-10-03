using CoreModule.Application.Courses.Episodes;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections.Episodes
{
    [ServiceFilter(typeof(TeacherActionFilter))]
    [BindProperties]
    public class AddModel : BaseRazor
    {
        private readonly ICourseFacade _courseFacade;

        public AddModel(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string EnglishTitle { get; set; }

        [Display(Name = "مدت زمان")]
        [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public TimeSpan TimeSpan { get; set; }

        [Display(Name = "فایل ویدئو")]
        [FileType("mp4",ErrorMessage = "{0} نامعتبر است لطفا با فرمت mp4 ارسال کنید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile VideoFile { get; set; }

        [Display(Name = "فایل ضمیمه")]
        [FileType("rar", ErrorMessage = "{0} نامعتبر است لطفا با فرمت rar ارسال کنید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile? AttachmentFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Guid courseId, Guid sectionId)
        {
            var result = await _courseFacade.AddEpisode(new AddEpisodeCommand(courseId, sectionId, Title,
                TimeSpan, VideoFile, AttachmentFile, false, EnglishTitle));

            return RedirectAndShowAlert(result, RedirectToPage("../Index", new { courseId }));
        }
    }
}
