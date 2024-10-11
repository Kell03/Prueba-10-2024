using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Modeldto
{
    public class PaginatedListdto<T>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }
        public List<T> Data { get; init; }


        public PaginatedListdto(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (pageSize > 0) ? (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize) : 0;
        }

    }
}
