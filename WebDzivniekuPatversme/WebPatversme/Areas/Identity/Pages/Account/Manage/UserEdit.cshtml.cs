using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserEditModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            Input = new UserEditViewModel();
        }

        [BindProperty]
        public UserEditViewModel Input { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(string userName)
        {
            Input.User = _userManager.Users
                .Where(x => x.UserName == userName)
                .FirstOrDefault();

            Input.Roles = _roleManager.Roles
                .Select(x => x.Name)
                .ToList();

            Input.Role = _userManager
                .GetRolesAsync(Input.User).Result
                .FirstOrDefault()
                .ToString();

            return Page();
        }
    }
}