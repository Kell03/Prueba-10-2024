using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IAuditoriaService
    {
        Task<Result<AuditoriaDto>> Add(ProductoDto productoDto, string accion, int? cantidad = 0); // Método para agregar un nuevo registro de auditoría
        Task<Result<bool>> Delete(int id); // Método para eliminar un registro de auditoría por ID
        Task<Result<PaginatedList<AuditoriaDto>>> GetAll(); // Método para obtener todos los registros de auditoría (paginados)
        Task<Result<AuditoriaDto>> GetById(int id); // Método para obtener un registro de auditoría por ID
        Task<Result<AuditoriaDto>> Update(ProductoDto productodto); // Método para actualizar un registro de auditoría por ID


    }
}
