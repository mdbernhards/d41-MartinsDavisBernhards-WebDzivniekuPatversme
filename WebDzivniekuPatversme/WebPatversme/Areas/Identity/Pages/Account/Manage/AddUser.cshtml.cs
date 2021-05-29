using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

using System.Text.Encodings.Web;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class AddUserModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AddUserModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;

            Input = new UserEditViewModel();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UserEditViewModel Input { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Input.Roles = _roleManager.Roles
                .Select(x => x.Name).ToList();

            Roles = _roleManager.Roles.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddUserAsync(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string returnUrl = Url.Content("~/");

            var user = new ApplicationUser { UserName = model.User.Email, Email = model.User.Email, Name = model.User.Name, Surname = model.User.Surname, PhoneNumber = model.User.PhoneNumber };
            var pass = GeneratePassword();
            var result = await _userManager.CreateAsync(user, pass);

            await _userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders
                    .Base64UrlEncode(Encoding.UTF8
                    .GetBytes(code));

                var callbackUrl = (Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code, returnUrl },
                    protocol: Request.Scheme));

                await _emailSender.SendEmailAsync(user.Email, "Apstiprināšanas E-pasts WebPatversme",
                    $"Lūdzu apstipriniet savu profilu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>spiežot šeit</a>. Jūsu parole ir: " + pass);

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = user.Email, returnUrl });
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

            return RedirectToPage("UserControl");
        }

        private string GeneratePassword()
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + "2@";
        }
    }
}