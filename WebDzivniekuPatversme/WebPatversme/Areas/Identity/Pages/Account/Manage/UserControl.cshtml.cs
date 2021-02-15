using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class UserControlModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserControlModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public List<UserWithRole> UsersWithRole { get; set; }

        public List<IdentityUser> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public class UserWithRole
        {
            public IdentityUser User { get; set; }

            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();

            UsersWithRole = new List<UserWithRole>();

            foreach (var user in Users)
            {
                UsersWithRole.Add(
                    new UserWithRole() 
                    { 
                        Role = _userManager.GetRolesAsync(user).Result[0], 
                        User = user
                    });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostChangeRolesAsync(string role, string userName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(userName).Result;
            var usersRoles = _userManager.GetRolesAsync(user).Result;

            await _userManager.RemoveFromRolesAsync(
                user,
                usersRoles);

            await _userManager.AddToRoleAsync(user, role);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(userName).Result;
            await _userManager.DeleteAsync(user);

            return RedirectToPage();
        }
    }
}