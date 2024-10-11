using Capa_Acceso_Datos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Acceso_Datos.Repository
{
    public interface IRepository<T>
    {
        Task<Result<T>> Add(T entity);
        Task<Result<T>> Update(T entity);
        Task<Result<bool>> Delete(int entityId);
        Task<Result<PaginatedList<T>>> GetAll(GetRequest<T>? request = null, int? pageIndex = 0, int? pageSize = 0);
        Task<Result<T>>? GetById(int entityId);

        Task<Result<bool>> DeleteAllById(GetRequest<T>? request = null);

    }
}
