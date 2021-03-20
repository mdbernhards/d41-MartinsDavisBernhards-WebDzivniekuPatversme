using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public EmailViewModel Input { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new EmailViewModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nevar ielādēt lietotāju ar ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nevar ielādēt lietotāju ar ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);

                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);

            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId, email = Input.NewEmail, code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Apstiprini savu E-pastu",
                    $"Lūdzu apstiprini savu E-pastu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>spiežot šeit</a>.");

                StatusMessage = "Nosūtīta apstiprināšanas saite, lai mainītu E-pastu. Lūdzu pārbaudiet savu E-pastu.";

                return RedirectToPage();
            }

            StatusMessage = "Tavs E-pasts netika mainīts.";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nevar ielādēt lietotāju ar ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);

                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId, code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(
                email,
                "Apstiprini savu E-pastu",
                $"Lūdzu apstiprini savu E-pastu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>spiežot šeit</a>.");

            StatusMessage = "Verifikācijas E-pasts nosūtīts. Lūdzu pārbaudiet savu E-pastu.";

            return RedirectToPage();
        }
    }
}