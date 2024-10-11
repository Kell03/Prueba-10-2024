using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_Acceso_Datos.Utils
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }
        public List<T> Data { get; init; }

        public PaginatedList(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (pageSize > 0) ? (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize) : 0;
        }

        //public PaginatedList(List<T> items, int pageIndex, int totalPages)
        //{
        //    Items = items;
        //    PageIndex = pageIndex;
        //    TotalPages = totalPages;
        //}
    }
}
