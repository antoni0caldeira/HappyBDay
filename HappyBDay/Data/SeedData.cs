using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyBDay.Models;

namespace HappyBDay.Data
{
    public class SeedData
    {

        private const string AdminUser = "admin@bbdl.com";
        private const string AdminPassword = "Password#123";

        private const string Role_Admin = "Admin";
        private const string Role_User = "User";


        internal static void InsertFakeData(HappyBDayContext DbContext)
        {
            InsertFakeProfile(DbContext);
            InsertFakeUsersDbAsync(DbContext);
            InsertFakeDepartments(DbContext);
            InsertFakeConsultants(DbContext);
        }

        internal static async Task InsertFakeUsersAsync(UserManager<IdentityUser> userManager)
        {
                       
            IdentityUser user = await CreateUserIfDontExists(userManager, "Bernardo@bbdl.com", "Password#123");
            await AddUserRoleIfNeeded(userManager, user, Role_User);


            IdentityUser user1 = await CreateUserIfDontExists(userManager, "Luis@bbdl.com", "Password#123");
            await AddUserRoleIfNeeded(userManager, user1, Role_User);


            IdentityUser user2 = await CreateUserIfDontExists(userManager, "David@bbdl.com", "Password#123");
            await AddUserRoleIfNeeded(userManager, user2, Role_User);

        }
        internal static void InsertFakeUsersDbAsync(HappyBDayContext DbContext)
        {
            if (DbContext.Users.Any()) return;

            Profile user = DbContext.Profile.FirstOrDefault(c => c.Name == "User");
            DbContext.Users.AddRange(new Users[]
            {
                new Users
                {
                    Username = "Bernardo",
                    IdProfileNavigation = user,

                },
                new Users
                {
                    Username = "Luís",
                    IdProfileNavigation = user,

                },
                new Users
                {
                    Username = "David",
                    IdProfileNavigation = user,

                },
            });
            DbContext.SaveChanges();
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

       

        private static void InsertFakeConsultants(HappyBDayContext DbContext)
        {
            if (DbContext.Consultants.Any()) return;

            for (int i = 0; i < 50; i++)
            {
               
                Departments dep = DbContext.Departments.FirstOrDefault(c => c.Name == "BackEnd Department");

                DbContext.Consultants.Add(
                    new Consultants {
                        
                        Name = "Rita"+ i,
                        Email = "rita.cap@bbdl.com"+i,
                        Phone = Convert.ToString(915469887 +i),
                        Status = true,
                        DateOfBirth = new DateTime(1950+i, 05, 05),
                        ConsultantNumber = 1+i,
                        Gender = "Female",
                        IdDepartmentsNavigation = dep,

                    });
                DbContext.SaveChanges();

            }  
            


        }

        internal static void InsertFakeDepartments(HappyBDayContext DbContext)
        {
            if (DbContext.Departments.Any()) return;

            DbContext.Departments.AddRange(new Departments[] {
            new Departments
            {
                Name = "BackEnd Department"
            }
            });
            DbContext.SaveChanges();

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

        internal static void InsertFakeProfile(HappyBDayContext DbContext)
        {
            if (DbContext.Profile.Any()) return;
            DbContext.Profile.AddRange(new Profile[] {
            new Profile
            {
                Name= "User",
            }
            }) ;
            DbContext.SaveChanges();
        }

       
    }
}
