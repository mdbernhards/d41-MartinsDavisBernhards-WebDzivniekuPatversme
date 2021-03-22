using System;
using System.Linq;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebDzivniekuPatversme.Services.Other
{
    public class UserChecker
    {
        private readonly ApplicationDbContext _context;

        public UserChecker(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void RoleChecker()
        {
            CheckIfAllRolesExist();
            CheckIfAdministratorExists();
            CheckIfAllUsersHaveRoles();

            await _context.SaveChangesAsync();
        }

        private async void CheckIfAllRolesExist()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "administrator"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "administrator", NormalizedName = "administrator" });
            }

            if (!_context.Roles.Any(r => r.Name == "worker"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "worker", NormalizedName = "worker" });
            }

            if (!_context.Roles.Any(r => r.Name == "user"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "user", NormalizedName = "user" });
            }
        }

        private async void CheckIfAdministratorExists()
        {
            var user = new ApplicationUser
            {
                UserName = "martinsdavisbernhards@gmail.com",
                NormalizedUserName = "martinsdavisbernhards@gmail.com",
                Email = "martinsdavisbernhards@gmail.com",
                NormalizedEmail = "martinsdavisbernhards@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Users.Any(u => u.Email == user.Email))
            {
                var password = new PasswordHasher<IdentityUser>();
                var userStore = new UserStore<IdentityUser>(_context);
                var hashed = password.HashPassword(user, "password");

                user.PasswordHash = hashed;

                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "administrator");
            }
        }

        private async void CheckIfAllUsersHaveRoles()
        {
            var users = _context.Users.ToList();
            var userStore = new UserStore<IdentityUser>(_context);

            foreach (var user in users)
            {
                var role = userStore.GetRolesAsync(user).Result.ToList().FirstOrDefault();

                if (string.IsNullOrEmpty(role))
                {
                    await userStore.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}