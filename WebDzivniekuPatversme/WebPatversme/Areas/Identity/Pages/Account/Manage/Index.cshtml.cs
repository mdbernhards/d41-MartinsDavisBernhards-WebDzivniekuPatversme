using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Validation;

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
        public InputModel Input { get; set; }

        [BindProperty]
        public OutputModel Output { get; set; }

        public class InputModel
        {
            [StringLength(50, ErrorMessage = "Vai jūsu vārds tiešām ir tik garš?")]
            [Display(Name = "Vārds")]
            public string Name { get; set; }

            [StringLength(50, ErrorMessage = "Vai jūsu uzvārds tiešām ir tik garš?")]
            [Display(Name = "Uzvārds")]
            public string Surname { get; set; }

            [Phone]
            [Display(Name = "Telefona numurs")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Attēls")]
            [DataType(DataType.Upload)]
            [MaxFileSizeValidation(6 * 1024 * 1024)]
            [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" })]
            public IFormFile Image { set; get; }
        }

        public class OutputModel
        {
            public string ImageString { set; get; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
            };

            Output = new OutputModel
            {
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