using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IUsuarioService
    {
        Task<Result<UsuarioDto>> Add(UsuarioDto usuario); // Método para agregar un nuevo usuario
        Task<Result<bool>> Delete(int id); // Método para eliminar un usuario por ID
        Task<Result<PaginatedList<UsuarioDto>>> GetAll(); // Método para obtener todos los usuarios (paginados)
        Task<Result<UsuarioDto>> GetById(int id); // Método para obtener un usuario por ID
        Task<Result<UsuarioDto>> Update(int id, UsuarioDto usuario); // Método para actualizar un usuario por ID

    }
}
