using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Identity;
using System;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class AddUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AddUserModel(
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

            var user = new ApplicationUser { UserName = model.User.UserName, Email = model.User.Email, Name = model.User.Name, Surname = model.User.Surname, PhoneNumber = model.User.PhoneNumber };
            var result = await _userManager.CreateAsync(user, GeneratePassword());

            await _userManager.AddToRoleAsync(user, model.Role);

            StatusMessage = "Jūsu profils tika atjaunots";

            return RedirectToPage("UserControl");
        }

        private string GeneratePassword()
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}