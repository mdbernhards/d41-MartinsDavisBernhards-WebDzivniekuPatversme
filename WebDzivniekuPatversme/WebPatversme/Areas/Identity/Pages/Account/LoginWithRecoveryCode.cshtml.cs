using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        public LoginWithRecoveryCodeModel(
            SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public LoginWithRecoveryCodeViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Nevar ielādēt Divu-Soļu autentifikācijas lietotāju.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Nevar ielādēt Divu-Soļu autentifikācijas lietotāju.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);
            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("lietotājs ar ID '{UserId}' ienāca ar Divu-Soļu autentifikāciju.", user.Id);

                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("lietotāja ar ID '{UserId}' profils tika aizslēgts.", user.Id);

                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Nederīgs autentifikācijas kods tika ievadīts lietotājam ar ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Nederīgs Autentifikācijas kods.");

                return Page();
            }
        }
    }
}