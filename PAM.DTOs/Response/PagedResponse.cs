using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<T> Data { get; set; }
    }
}
