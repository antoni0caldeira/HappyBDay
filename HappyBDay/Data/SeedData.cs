using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Data
{
    public class SeedData
    {
        private const string AdminUser = "admin@bbdl.com";
        private const string AdminPassword = "Password#123";

        internal static async Task InsertStandartAdmin(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByNameAsync(AdminUser);

            if (user == null)
            {

                user = new IdentityUser(AdminUser);
                await userManager.CreateAsync(user, AdminPassword);
            }
        }
    }
}
