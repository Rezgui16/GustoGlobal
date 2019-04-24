using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gusto.Class
{
    public static class InitializerRoles
    {
        public static async Task Initial(RoleManager<IdentityRole> roleManager)
        {

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var users = new IdentityRole("Admin");
                await roleManager.CreateAsync(users);
            }

            if (!await roleManager.RoleExistsAsync("Chef"))
            {
                var users = new IdentityRole("Chef");
                await roleManager.CreateAsync(users);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var users = new IdentityRole("User");
                await roleManager.CreateAsync(users);
            }
        }
    }
}
