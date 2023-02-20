using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile
{
    [BindProperties]
    public class EditModel : BaseRazor
    {
        private readonly IUserFacade _userFacade;

        public EditModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public async Task OnGet()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            if (user != null)
            {
                Name = user.Name;
                Family = user.Family;
                Email = user.Email;
            }

        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _userFacade.EditUser(new UserModule.Core.Commands.Users.Edit.EditUserCommand()
            {
                UserId = User.GetUserId(),
                Name = Name,
                Family = Family,
                Email = Email
            });


            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
    }
}
