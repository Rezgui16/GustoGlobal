using GustoLib.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gusto.Class
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(GustoDbContext context, IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Chef", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //var UserManager = serviceProvider.GetRequiredService <UserManager<User>>();
            //var SignInManager = serviceProvider.GetRequiredService<SignInManager<User>>();
            //if (!await UserManager.GetUserName(jeff@test.fr)) { }
            //    var user = new User()
            //{
            //    UserName = "jeff@test.fr",
            //    Email = "jeff@test.fr",
            //    LastName = "jeff",
            //    FirstMidName = "test"
            //};
            //await UserManager.AddToRoleAsync(user, "User");
            //await SignInManager.SignInAsync(user, isPersistent: false);
        }
    }
}
