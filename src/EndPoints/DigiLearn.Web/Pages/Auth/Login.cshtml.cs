using Common.Application.SecurityUtil;
using DigiLearn.Web.Infrastructure.JwtUtil;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth
{
    [BindProperties]
    public class LoginModel : BaseRazor
    {
        private readonly IUserFacade _userFacade;
        private readonly IConfiguration _configuration;

        public LoginModel(IUserFacade userFacade, IConfiguration configuration)
        {
            _userFacade = userFacade;
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(5, ErrorMessage = " کلمه عبور باید بیشتر از 5 کارکتر باشد")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetUserByPhoneNumber(PhoneNumber);
            if (user == null)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            if (Sha256Hasher.IsCompare(user.Password, Password) == false)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);

            if(RememberMe)
            {

                HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(30),
                    Secure = true
                });
            }
            else
            {

                HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true
                });
            }


            return RedirectToPage("../Index");
        }
    }
}
