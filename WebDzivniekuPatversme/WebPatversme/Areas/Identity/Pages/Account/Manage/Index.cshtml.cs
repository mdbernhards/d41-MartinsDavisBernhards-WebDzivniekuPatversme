using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment appEnvironment,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public IndexViewModel Input { get; set; }

        [BindProperty]
        public IndexViewModel Output { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new IndexViewModel
            {
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ImageString = user.ImagePath,
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

            if(Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if(Input.Surname != user.Surname)
            {
                user.Surname = Input.Surname;
            }

            if(Input.Image != null)
            {
                user.ImagePath = SaveImage(user);
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);

                return Page();
            }

            await _userManager.UpdateAsync(user);

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);

                return Page();
            }

            await _context.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Jūsu profils tika atjaunots";

            return RedirectToPage();
        }

        private string SaveImage(ApplicationUser user)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\users");

            if (Input.Image != null && Input.Image.Length > 0)
            {
                var fileName = Path.GetFileName(user.Id + Path.GetExtension(Input.Image.FileName));
                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);

                Input.Image.CopyTo(fileStream);
                fileStream.Close();

                user.ImagePath = fileName;

                return fileName;
            }

            return null;
        }
    }
}