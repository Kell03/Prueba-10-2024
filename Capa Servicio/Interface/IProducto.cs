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
    public interface IProducto
    {
        // Método para agregar un nuevo producto
        Task<Result<Productodto>> Add(Producto productoDto);

        // Método para obtener todos los productos
        Task<Result<PaginatedListdto<Productodto>>> GetAll(int index, int take);

        // Método para obtener un producto por su ID
        Task<Result<Productodto>> GetById(int id);

        // Método para actualizar un producto existente
        Task<Result<Productodto>> Update(Producto productoDto);

        // Método para eliminar un producto por su ID
        Task<Result<bool>> Delete(int id);
    }
}
