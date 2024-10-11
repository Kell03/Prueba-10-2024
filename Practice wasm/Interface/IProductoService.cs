using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IProductoService
    {
        Task<Result<ProductoDto>> Add(ProductoDto producto); // Método para agregar un nuevo producto
        Task<Result<bool>> Delete(int id); // Método para eliminar un producto por ID
        Task<Result<PaginatedList<ProductoDto>>> GetAll(); // Método para obtener todos los productos (paginados)
        Task<Result<ProductoDto>> GetById(int id); // Método para obtener un producto por ID
        Task<Result<ProductoDto>> Update(int id, ProductoDto producto); // Método para actualizar un producto por ID

    }

}
