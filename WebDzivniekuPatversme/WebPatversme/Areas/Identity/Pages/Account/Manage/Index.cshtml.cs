using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vārds ir obligāts.")]
            [StringLength(50, ErrorMessage = "Vai jūsu vārds tiešām ir tik garš?")]
            [Display(Name = "Vārds")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Uzvārds ir obligāts.")]
            [StringLength(50, ErrorMessage = "Vai jūsu uzvārds tiešām ir tik garš?")]
            [Display(Name = "Uzvārds")]
            public string Surname { get; set; }

            [Phone]
            [Display(Name = "Telefona numurs")]
            public string PhoneNumber { get; set; }

            public string ImagePath { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
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

        public async Task<IActionResult> OnPostAsync()
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

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Negaidīta kļūme saglabājot telefona numuru.";

                    return RedirectToPage();
                }
            }

            var name = await _userManager.GetUserNameAsync(user);
            var userName = Input.Surname + ", " + Input.Name;
            if (userName != name)
            {
                await _userManager.SetUserNameAsync(user, userName);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Jūsu profils tika atjaunots";

            return RedirectToPage();
        }
    }
}