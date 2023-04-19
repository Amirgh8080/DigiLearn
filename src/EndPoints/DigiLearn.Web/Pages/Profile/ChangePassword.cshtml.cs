using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Services;
using UserModule.Core.Commands.Users.ChangePassword;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class ChangePasswordModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public ChangePasswordModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = " {0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = " {0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = " {0} را وارد کنید")]
    [Compare("NewPassword",ErrorMessage = "کلمه های عبور یکسان نیستند")]
    [DataType(DataType.Password)]
    public string RepeatNewPassword { get; set; }

    public void OnGet()
    { 

    }


    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.ChangegUserPassword(new ChangeUserPasswordCommand()
        {
            UserId = User.GetUserId(),
            CurrentPassword = CurrentPassword,
            NewPassword = NewPassword
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
