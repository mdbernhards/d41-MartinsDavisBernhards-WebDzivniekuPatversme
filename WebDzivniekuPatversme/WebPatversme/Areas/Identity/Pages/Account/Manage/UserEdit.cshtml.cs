using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserEditModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

            Input = new UserEditViewModel();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UserEditViewModel Input { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(string userName)
        {
            Input.User = _userManager.Users
                .Where(x => x.UserName == userName).FirstOrDefault();

            Input.Roles = _roleManager.Roles
                .Select(x => x.Name).ToList();

            Input.Role = _userManager
                .GetRolesAsync(Input.User).Result.FirstOrDefault().ToString();

            Roles = _roleManager.Roles.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostEditUserAsync(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(model.User.UserName).Result;
            var usersRoles = _userManager.GetRolesAsync(user).Result;

            await _userManager.RemoveFromRolesAsync(user, usersRoles);
            await _userManager.AddToRoleAsync(user, model.Role);
            await _userManager.SetPhoneNumberAsync(user, model.User.PhoneNumber);
            await _userManager.SetEmailAsync(user, model.User.Email);
            await _userManager.SetUserNameAsync(user, model.User.Email);


            if (model.User.Name != user.Name)
            {
                user.Name = model.User.Name;
            }

            if (model.User.Surname != user.Surname)
            {
                user.Surname = model.User.Surname;
            }

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            StatusMessage = "Jūsu profils tika atjaunots";

            return RedirectToPage("UserControl");
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(model.User.UserName).Result;

            await _userManager.DeleteAsync(user);

            return RedirectToPage("UserControl");
        }
    }
}