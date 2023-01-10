using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Supply.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Sales.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Storekeeper.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Dimka",
                LastName = "Baymurzin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "X12x+X12x+");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Administrator.ToString());
                }

            }
        }
    }
}
