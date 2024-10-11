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
    public interface IInventario
    {
        // Método para agregar un nuevo registro de inventario
        Task<Result<Inventariodto>> Add(Inventario inventarioDto);

        // Método para obtener todos los registros de inventario
        Task<Result<PaginatedListdto<Inventariodto>>> GetAll(int index, int take);

        // Método para obtener un registro de inventario por su ID
        Task<Result<Inventariodto>> GetById(int id);

        // Método para actualizar un registro de inventario existente
        Task<Result<Inventariodto>> Update(Inventario inventarioDto);

        // Método para eliminar un registro de inventario por su ID
        Task<Result<bool>> Delete(int id);

        //Metodo para obtener ultimo producto registrado por su Id
        Task<Result<Inventariodto>> getExitsProduct(int id);

        //Metodo para eliminar  todos los productos registrados por su Id

        Task<Result<bool>> DeleteAllById(int id);

    }
}
