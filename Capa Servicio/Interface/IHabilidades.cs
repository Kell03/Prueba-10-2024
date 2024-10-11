using Capa_Acceso_Datos.Utils;
using CapaDominio;
using CapaDominio.Modeldto;
using CapaDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Servicio.Interface
{
    public interface IHabilidades
    {


        // Método para agregar una nueva habilidad
        Task<Result<Habilidades>> Add(Habilidades habilidadDto);

        // Método para obtener todas las habilidades
        Task<Result<PaginatedListdto<Habilidadesdto>>> GetAll(int index, int take);

        // Método para obtener una habilidad por su ID
        Task<Result<Habilidadesdto>> GetById(int id);

        // Método para actualizar una habilidad existente
        Task<Result<Habilidadesdto>> Update(Habilidades habilidadDto);

        // Método para eliminar una habilidad por su ID
        Task<Result<bool>> Delete(int id);
    }
}
