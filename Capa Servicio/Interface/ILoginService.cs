using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Servicio.Interface
{
    public interface ILoginService
    {

            Task<Usuario> Login(string nombre, string password);
        

    }
}
