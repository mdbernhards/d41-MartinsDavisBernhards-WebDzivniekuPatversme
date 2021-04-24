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
    public class UserControlModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserControlModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public List<UserControlViewModel> Input { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public string Search { get; set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            Search = name;
            Roles = _roleManager.Roles.ToList();
            Input = new List<UserControlViewModel>();

            var users = FilterUsers(name, _userManager.Users.ToList());

            foreach (var user in users)
            {
                var role = _userManager
                    .GetRolesAsync(user).Result.FirstOrDefault().ToString();

                Input.Add(new UserControlViewModel() 
                { 
                    Role = role, 
                    User = user,
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(model.User.UserName).Result;

            await _userManager.DeleteAsync(user);

            return RedirectToPage();
        }

        private List<ApplicationUser> FilterUsers(string name, List<ApplicationUser> users)
        {
            var filteredUsers = new List<ApplicationUser>();

            if (!string.IsNullOrEmpty(name))
            {
                filteredUsers.AddRange(users.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList());
                filteredUsers.AddRange(users.Where(x => x.Surname.ToLower().Contains(name.ToLower())).ToList());
                filteredUsers.AddRange(users.Where(x => x.Email.ToLower().Contains(name.ToLower())).ToList());
                filteredUsers.AddRange(users.Where(x => x.PhoneNumber.ToLower().Contains(name.ToLower())).ToList());

                foreach (var user in users)
                {
                    var role = _userManager
                        .GetRolesAsync(user).Result.FirstOrDefault().ToString();

                    if (role.Contains(name))
                    {
                        filteredUsers.Add(user);
                    }
                }
            }
            else
            {
                return users;
            }

            return filteredUsers.Distinct().ToList();
        }
    }
}