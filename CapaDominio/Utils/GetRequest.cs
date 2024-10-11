using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Acceso_Datos.Utils
{
    public class GetRequest<T> 
    {
        public Expression<Func<T, bool>>? Filter { get; set; } = null;
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; } = null;
        public int? Skip { get; set; } = null;
        public int? Take { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // Para incluir relaciones

    }
}
