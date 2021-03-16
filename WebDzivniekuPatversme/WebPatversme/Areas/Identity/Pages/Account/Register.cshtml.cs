using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _appEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                if (Input.UserName == null)
                {
                    Input.UserName = Input.Email;
                }

                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, Name = Input.Name, Surname = Input.Surname, PhoneNumber = Input.PhoneNumber};
                var result = await _userManager.CreateAsync(user, Input.Password);

                await _userManager.AddToRoleAsync(user, "user");

                if (result.Succeeded)
                {
                    _logger.LogInformation("Lietotājs izveidoja jaunu profilu ar paroli.");

                    SaveImageAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = (Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme));

                    await _emailSender.SendEmailAsync(Input.Email, "Apstiprināšanas E-pasts WebPatversme",
                        $"Lūdzu apstipriniet savu profilu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>spiežot šeit</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private async void SaveImageAsync()
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\users");

            if (Input.Image != null && Input.Image.Length > 0)
            {
                var fileName = Path.GetFileName(user.Name + user.Id + Path.GetExtension(Input.Image.FileName));
                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);

                System.IO.File.Delete(Path.Combine(uploads, fileName));
                Input.Image.CopyTo(fileStream);
                fileStream.Close();

                user.ImagePath = fileName;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}