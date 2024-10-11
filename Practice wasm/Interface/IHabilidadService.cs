using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IHabilidadService
    {
        Task<Result<HabilidadDto>> Add(HabilidadDto habilidad); // Cambia Create por Add
        Task<Result<bool>> Delete(int id);
        Task<Result<PaginatedList<HabilidadDto>>> GetAll();
        Task<Result<HabilidadDto>> GetById(int id);
        Task<Result<HabilidadDto>> Update(int id, HabilidadDto habilidad);
    }
}
