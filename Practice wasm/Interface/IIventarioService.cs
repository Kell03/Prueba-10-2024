using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IInventarioService
    {
        Task<Result<InventarioDto>> Add(ProductoDto productoDto); // Método para agregar un nuevo registro de inventario
        Task<Result<bool>> Delete(int id); // Método para eliminar un registro de inventario por ID
        Task<Result<PaginatedList<InventarioDto>>> GetAll(); // Método para obtener todos los registros de inventario (paginados)
        Task<Result<InventarioDto>> GetById(int id); // Método para obtener un registro de inventario por ID
        Task<Result<InventarioDto>> Update( ProductoDto productodto); // Método para actualizar un registro de inventario por ID
    }
}
