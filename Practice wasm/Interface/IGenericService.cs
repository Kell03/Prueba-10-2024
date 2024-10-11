using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    
        public interface IGenericService<T> where T : class
        {
            Task<Result<PaginatedList<T>>> GetAll( string? additionalPath = "");
            Task<Result<T>> GetById(int id, string? additionalPath = "");
            Task<Result<T>> Add(T entity, string? additionalPath = "");
            Task<Result<T>> Update(int id, T entity, string? additionalPath = "");
        Task<Result<bool>> Delete(int id, string? additionalPath = "");
        }
    
}
