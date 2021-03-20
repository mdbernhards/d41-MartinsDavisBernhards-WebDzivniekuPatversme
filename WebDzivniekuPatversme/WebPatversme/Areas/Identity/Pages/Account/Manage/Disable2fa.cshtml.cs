using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nevar ielādēt lietotāju ar ID '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"Nevar atslēgt Divu-Soļu verifikāciju lietotājam ar ID '{_userManager.GetUserId(User)}', jo tā pašlaik nav ieslēgta.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nevar ielādēt lietotāju ar ID '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);

            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Negaidīta kļūda notika atslēdzot Divu-Soļu verifikāciju lietotājam ar ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("Lietotājs ar ID '{UserId}' atslēdza savu Divu-Soļu verifikāciju.", _userManager.GetUserId(User));
            StatusMessage = "Divu-Soļu verifikācija ir atslēgta. Jūs varat ieslēgt Divu-Soļu verifikāciju, kad sagatavojat autentifikācijas aplikāciju";

            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}