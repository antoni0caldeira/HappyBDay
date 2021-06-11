using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Data
{
    public class SeedData
    {

        private const string AdminUser = "admin@bbdl.com";
        private const string AdminPassword = "Password#123";

        private const string Role_Admin = "Admin";
        private const string Role_User = "User";

        internal static async Task InsertFakeUsersAsync(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await CreateUserIfDontExists(userManager, "Bernardo@bbdl.com", "Password#123");
            await AddUserRoleIfNeeded(userManager, user, Role_User);

        }

        internal static async Task InsertRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleIfNecessarie(roleManager, Role_Admin);
            await CreateRoleIfNecessarie(roleManager, Role_User);
        }


        private static async Task CreateRoleIfNecessarie(RoleManager<IdentityRole> roleManager, string func)
        {
            if(!await roleManager.RoleExistsAsync(func)) 
            {
                IdentityRole role = new IdentityRole(func);
                await roleManager.CreateAsync(role);
            }
        }

        internal static async Task InsertStandartAdmin(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await CreateUserIfDontExists(userManager, AdminUser, AdminPassword);
            await AddUserRoleIfNeeded(userManager, user, Role_Admin);
        
        }

        private static async Task AddUserRoleIfNeeded (UserManager<IdentityUser> userManager, IdentityUser user , string role)
        {

            if(!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }

        private static async Task<IdentityUser> CreateUserIfDontExists(UserManager<IdentityUser> userManager, string userName, string password)
        {
            IdentityUser user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser(userName);
                await userManager.CreateAsync(user, password);
            }
            return user; 
        }


    }
}
