using System;
using System.Linq;
using WebDzivniekuPatversme.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class UserChecker
{
    private ApplicationDbContext _context;

    public UserChecker(ApplicationDbContext context)
    {
        _context = context;
    }

    public async void RoleChecker()
    {
        var user = new IdentityUser
        {
            UserName = "martinsdavisbernhards@gmail.com",
            NormalizedUserName = "martinsdavisbernhards@gmail.com",
            Email = "martinsdavisbernhards@gmail.com",
            NormalizedEmail = "martinsdavisbernhards@gmail.com",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

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

        if (!_context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<IdentityUser>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;
            var userStore = new UserStore<IdentityUser>(_context);
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "administrator");
        }

        await _context.SaveChangesAsync();
    }
}