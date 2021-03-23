﻿using System.Linq;
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

        public async Task<IActionResult> OnGetAsync()
        {
            var users = _userManager.Users.ToList();

            Roles = _roleManager.Roles.ToList();
            Input = new List<UserControlViewModel>();

            foreach (var user in users)
            {
                var role = _userManager
                    .GetRolesAsync(user).Result
                    .FirstOrDefault()
                    .ToString();

                Input.Add(new UserControlViewModel() 
                { 
                    Role = role, 
                    User = user,
                });
            }

            return Page();
        }
    }
}