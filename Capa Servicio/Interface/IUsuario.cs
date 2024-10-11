using CapaDominio.Modeldto;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Acceso_Datos.Utils;

namespace Capa_Servicio.Interface
{
    public interface IUsuario
    {
        // Método para agregar un nuevo usuario
        Task<Result<Usuariodto>> Add(Usuario usuarioDto);

        // Método para obtener todos los usuarios
        Task<Result<PaginatedListdto<Usuariodto>>> GetAll(int index, int take);

        // Método para obtener un usuario por su ID
        Task<Result<Usuariodto>> GetById(int id);

        // Método para actualizar un usuario existente
        Task<Result<Usuariodto>> Update(Usuario usuarioDto);

        // Método para eliminar un usuario por su ID
        Task<Result<bool>> Delete(int id);

    }
}
