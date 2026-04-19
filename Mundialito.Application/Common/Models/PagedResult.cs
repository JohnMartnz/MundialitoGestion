using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Common.Models
{
    public record PagedResult<T>
    {
        public IEnumerable<T> Data { get; init; } = new List<T>();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
