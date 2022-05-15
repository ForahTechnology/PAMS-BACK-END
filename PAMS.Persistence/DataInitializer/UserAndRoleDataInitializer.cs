using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DataInitializer
{
    /// <summary>
    /// This static class initializes the default admin user and also create the user roles upon starting the application
    /// </summary>
    public static class UserAndRoleDataInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<PamsUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// All default users are seeded into the database in this static method.
        /// </summary>
        /// <param name="userManager"></param>
        private static void SeedUsers(UserManager<PamsUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@pams.com").Result == null)
            {
                PamsUser user = new PamsUser()
                {
                    FirstName = "Admin",
                    LastName = "Pams",
                    UserName = "admin@pams.com",
                    RegisteredDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "admin@pams.com",
                    EmailConfirmed = true,
                    Active = true,
                    LockoutEnabled = false
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin1.").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }

        }

        /// <summary>
        /// This static method creates all the necessary roles for users of this application.
        /// </summary>
        /// <param name="roleManager"></param>
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "SuperAdmin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Staff").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Staff";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }
    }

}
