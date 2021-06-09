using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Data
{
    public class SeedData
    {
        private const string NOME_UTILIZADOR_ADMIN_PADRAO = "admin@cap.com";
        private const string PASSWORD_UTILIZADOR_ADMIN_PADRAO = "teste";

        internal static async Task InsereAdministradorPadraoAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(NOME_UTILIZADOR_ADMIN_PADRAO);

            if (utilizador == null)
            {
                utilizador = new IdentityUser(NOME_UTILIZADOR_ADMIN_PADRAO);
                await gestorUtilizadores.CreateAsync(utilizador, PASSWORD_UTILIZADOR_ADMIN_PADRAO);
            }
        }


    }
}
