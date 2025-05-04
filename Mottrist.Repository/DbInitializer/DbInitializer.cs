﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Utilities.Identity;
using System.Text;
namespace Mottrist.Repository.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context ;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

                if (!_roleManager.RoleExistsAsync(AppUserRoles.RoleAdmin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleAdmin }).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleTraveler }).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleDriver }).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleEmployee }).GetAwaiter().GetResult();
                }

                if (!_context.ApplicationUsers.Any())
                {
                    ApplicationUser user = new();
                    user.FirstName = "admin";
                    user.LastName = "admin";
                    user.Email = "admin@mottrist.com";
                    user.UserName = "admin@mottrist.com";
                    user.LockoutEnabled = false;
                    user.IsAdmin = true;

                    var result = _userManager.CreateAsync(user, "Admin123@").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, AppUserRoles.RoleAdmin).GetAwaiter().GetResult();

                        // Email Confirmed
                        var codeToConfirm = _userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
                        codeToConfirm = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(codeToConfirm));

                        codeToConfirm = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(codeToConfirm));
                        _userManager.ConfirmEmailAsync(user, codeToConfirm).GetAwaiter().GetResult();

                        _userManager.ConfirmEmailAsync(user, codeToConfirm).GetAwaiter().GetResult();

                        // Set Lockout Enabled to false
                        _userManager.SetLockoutEnabledAsync(user, false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Something got wrong while initializing the database: {ex.Message}");
            }
        }
    }
}

