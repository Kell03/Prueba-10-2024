using Capa_Servicio.Interface;
using CapaDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Servicio.Services
{
    public class LoginService: ILoginService
    {
        ApipracticeContext dbContext = new ApipracticeContext();
        public LoginService(ApipracticeContext _dbcontext)
        {
            dbContext = _dbcontext;
        }

        public async Task<Usuario> Login(string nombre, string password)
        {

            IQueryable<Usuario> query = dbContext.Set<Usuario>();

            var model = await query.Where(x => x.NombreUsuario == nombre && x.Password == password).FirstAsync();
            return model;

        }
    }
}
