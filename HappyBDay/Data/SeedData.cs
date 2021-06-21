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
                    Email = "Bernardo@bbdl.com",
                    Status= true,

                },
                new Users
                {
                    Username = "Luís",
                    IdProfileNavigation = user,
                    Email = "Luis@bbdl.com",
                    Status= true,

                },
                new Users
                {
                    Username = "David",
                    IdProfileNavigation = user,
                    Email = "David@bbdl.com",
                    Status= true,

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

            Departments dep = DbContext.Departments.FirstOrDefault(c => c.Name == "BackEnd Department");
            DbContext.Consultants.AddRange(new Consultants[]
            {
                //new Consultants
                //{
                //    Name = "Bernardo José Costa Sousa",
                //    Email = "bernardojose.costasousa@altran.com",
                //    Phone = Convert.ToString(915469887),
                //    Status = true,
                //    DateOfBirth = new DateTime(1999, 05, 05),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Luís Henrique Diogo Bernardo",
                //    Email = "luishenrique.diogobernardo@altran.com",
                //    Phone = Convert.ToString(915469888),
                //    Status = true,
                //    DateOfBirth = new DateTime(1988, 05, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Ricardo Manuel Gonçalves Cabral",
                //    Email = "ricardomanuel.goncalvescabral@altran.com",
                //    Phone = Convert.ToString(915469890),
                //    Status = true,
                //    DateOfBirth = new DateTime(1995, 05, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Nuno Rafael Gonçalves Fonseca",
                //    Email = "nunorafael.goncalvesfonseca@altran.com",
                //    Phone = Convert.ToString(915469587),
                //    Status = true,
                //    DateOfBirth = new DateTime(1996, 06, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "João Pedro Gouveia Nunes",
                //    Email = "joaopedro.gouveianunes@altran.com",
                //    Phone = Convert.ToString(915469444),
                //    Status = true,
                //    DateOfBirth = new DateTime(1994, 03, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Vitaliy Hurskyy",
                //    Email = "vitaliy.hurskyy@altran.com",
                //    Phone = Convert.ToString(915449444),
                //    Status = true,
                //    DateOfBirth = new DateTime(1998, 08, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Pedro Daniel Lourenço Gaspar",
                //    Email = "pedrodaniel.lourencogaspar@altran.com",
                //    Phone = Convert.ToString(925449444),
                //    Status = true,
                //    DateOfBirth = new DateTime(1992, 08, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "António José Machado Caldeira",
                //    Email = "antoniojose.machadocaldeira@altran.com",
                //    Phone = Convert.ToString(925549444),
                //    Status = true,
                //    DateOfBirth = new DateTime(1970, 08, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Nuno Miguel Mimoso Aniceto",
                //    Email = "nunomiguel.mimosoaniceto@altran.com",
                //    Phone = Convert.ToString(925549555),
                //    Status = true,
                //    DateOfBirth = new DateTime(1975, 08, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Mariana Sofia Monteiro Santos",
                //    Email = "marianasofia.monteirosantos@altran.com",
                //    Phone = Convert.ToString(925544789),
                //    Status = true,
                //    DateOfBirth = new DateTime(1993, 04, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Patrícia Moura Gameiro",
                //    Email = "patricia.mouragameiro@altran.com",
                //    Phone = Convert.ToString(925544219),
                //    Status = true,
                //    DateOfBirth = new DateTime(1996, 11, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "André João Rodrigues Saavedra",
                //    Email = "andrejoao.rodriguessaavedra@altran.com",
                //    Phone = Convert.ToString(925577219),
                //    Status = true,
                //    DateOfBirth = new DateTime(1990, 11, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Nicolau Santos Barranha",
                //    Email = "nicolau.santosbarranha@altran.com",
                //    Phone = Convert.ToString(925577219),
                //    Status = true,
                //    DateOfBirth = new DateTime(1990, 11, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "João Manuel Santos Vieira",
                //    Email = "joaomanuel.santosvieira@altran.com",
                //    Phone = Convert.ToString(925469219),
                //    Status = true,
                //    DateOfBirth = new DateTime(1994, 02, 11),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "David Nunes Petrucci Silva Pinto",
                //    Email = "davidnunes.silvapinto@altran.com",
                //    Phone = Convert.ToString(925464719),
                //    Status = true,
                //    DateOfBirth = new DateTime(1982, 06, 27),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Bruno Nave",
                //    Email = "bruno.nave@altran.com",
                //    Phone = Convert.ToString(931547896),
                //    Status = true,
                //    DateOfBirth = new DateTime(1983, 08, 27),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Diogo Miguel Coluna",
                //    Email = "diogomiguel.coluna@altran.com",
                //    Phone = Convert.ToString(931547851),
                //    Status = true,
                //    DateOfBirth = new DateTime(1980, 10, 02),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Hugo Miguel Prata",
                //    Email = "hugomiguel.prata@altran.com",
                //    Phone = Convert.ToString(931547222),
                //    Status = true,
                //    DateOfBirth = new DateTime(1977, 03, 08),
                //    ConsultantNumber = 1,
                //    Gender = "Male",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Sara Leme",
                //    Email = "sara.leme@altran.com",
                //    Phone = Convert.ToString(911478596),
                //    Status = true,
                //    DateOfBirth = new DateTime(1986, 02, 01),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Rita Proença",
                //    Email = "rita.proenca@altran.com",
                //    Phone = Convert.ToString(921498632),
                //    Status = true,
                //    DateOfBirth = new DateTime(1979, 04, 15),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Beatriz Carpinteira Rodrigues",
                //    Email = "beatriz.carpinteirarodrigues@altran.com",
                //    Phone = Convert.ToString(924785965),
                //    Status = true,
                //    DateOfBirth = new DateTime(1995, 05, 05),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                //new Consultants
                //{
                //    Name = "Tânia Patrícia Barata",
                //    Email = "taniapatricia.barata@altran.com",
                //    Phone = Convert.ToString(924125595),
                //    Status = true,
                //    DateOfBirth = new DateTime(1998, 06, 08),
                //    ConsultantNumber = 1,
                //    Gender = "Female",
                //    IdDepartmentsNavigation = dep,
                //},
                new Consultants
                {
                    Name = "Bernardo José Costa Sousa",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915469887),
                    Status = true,
                    DateOfBirth = new DateTime(1999, 05, 05),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Luís Henrique Diogo Bernardo",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915469888),
                    Status = true,
                    DateOfBirth = new DateTime(1988, 05, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Ricardo Manuel Gonçalves Cabral",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915469890),
                    Status = true,
                    DateOfBirth = new DateTime(1995, 05, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Nuno Rafael Gonçalves Fonseca",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915469587),
                    Status = true,
                    DateOfBirth = new DateTime(1996, 06, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "João Pedro Gouveia Nunes",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915469444),
                    Status = true,
                    DateOfBirth = new DateTime(1994, 03, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Vitaliy Hurskyy",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(915449444),
                    Status = true,
                    DateOfBirth = new DateTime(1998, 08, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Pedro Daniel Lourenço Gaspar",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925449444),
                    Status = true,
                    DateOfBirth = new DateTime(1992, 08, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "António José Machado Caldeira",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925549444),
                    Status = true,
                    DateOfBirth = new DateTime(1970, 08, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Nuno Miguel Mimoso Aniceto",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925549555),
                    Status = true,
                    DateOfBirth = new DateTime(1975, 08, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Mariana Sofia Monteiro Santos",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925544789),
                    Status = true,
                    DateOfBirth = new DateTime(1993, 04, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Patrícia Moura Gameiro",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925544219),
                    Status = true,
                    DateOfBirth = new DateTime(1996, 11, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "André João Rodrigues Saavedra",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925577219),
                    Status = true,
                    DateOfBirth = new DateTime(1990, 11, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Nicolau Santos Barranha",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925577219),
                    Status = true,
                    DateOfBirth = new DateTime(1990, 11, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "João Manuel Santos Vieira",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925469219),
                    Status = true,
                    DateOfBirth = new DateTime(1994, 02, 11),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "David Nunes Petrucci Silva Pinto",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(925464719),
                    Status = true,
                    DateOfBirth = new DateTime(1982, 06, 27),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Bruno Nave",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(931547896),
                    Status = true,
                    DateOfBirth = new DateTime(1983, 08, 27),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Diogo Miguel Coluna",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(931547851),
                    Status = true,
                    DateOfBirth = new DateTime(1980, 10, 02),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Hugo Miguel Prata",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(931547222),
                    Status = true,
                    DateOfBirth = new DateTime(1977, 03, 08),
                    ConsultantNumber = "12345678",
                    Gender = "Male",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Sara Leme",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(911478596),
                    Status = true,
                    DateOfBirth = new DateTime(1986, 02, 01),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Rita Proença",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(921498632),
                    Status = true,
                    DateOfBirth = new DateTime(1979, 04, 15),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Beatriz Carpinteira Rodrigues",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(924785965),
                    Status = true,
                    DateOfBirth = new DateTime(1995, 05, 05),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },
                new Consultants
                {
                    Name = "Tânia Patrícia Barata",
                    Email = "uptelautomated@gmail.com",
                    Phone = Convert.ToString(924125595),
                    Status = true,
                    DateOfBirth = new DateTime(1998, 06, 08),
                    ConsultantNumber = "12345678",
                    Gender = "Female",
                    IdDepartmentsNavigation = dep,
                },

            });
            DbContext.SaveChanges();

            //for (int i = 0; i < 50; i++)
            //{

            //    Departments dep = DbContext.Departments.FirstOrDefault(c => c.Name == "BackEnd Department");

            //    DbContext.Consultants.Add(
            //        new Consultants {

            //            Name = "Rita"+ i,
            //            Email = "rita.cap@bbdl.com"+i,
            //            Phone = Convert.ToString(915469887 +i),
            //            Status = true,
            //            DateOfBirth = new DateTime(1950+i, 05, 05),
            //            ConsultantNumber = 1+i,
            //            Gender = "Female",
            //            IdDepartmentsNavigation = dep,

            //        });
            //    DbContext.SaveChanges();

            //}  



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
