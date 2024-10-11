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
    public interface IAuditoria
    {

        // Método para agregar un nuevo registro de auditoría
        Task<Result<Auditoriadto>> Add(Auditoria auditoriaDto);

        // Método para obtener todos los registros de auditoría
        Task<Result<PaginatedListdto<Auditoriadto>>> GetAll(int index, int take);

        // Método para obtener un registro de auditoría por su ID
        Task<Result<Auditoriadto>> GetById(int id);

        // Método para actualizar un registro de auditoría existente
        Task<Result<Auditoriadto>> Update(Auditoria auditoriaDto);

        // Método para eliminar un registro de auditoría por su ID
        Task<Result<bool>> Delete(int id);

        Task<Result<Auditoriadto>> getExitsProduct(int id);


    }
}
