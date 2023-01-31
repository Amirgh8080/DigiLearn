using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth
{
    public class LoginModel : BaseRazor
    {
        private readonly IUserFacade _userFacade;

        public LoginModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public void OnGet()
        {
        }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetUserByPhoneNumber(PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError(PhoneNumber, "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

           // var correctPassword =  _userFacade.


            return RedirectAndShowAlert(result);
        }
    }
}
