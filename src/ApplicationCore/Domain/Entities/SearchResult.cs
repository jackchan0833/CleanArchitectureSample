using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Domain.Entities
{
    public class SearchResult<T>
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalCount { get; set; }
        public List<T> Records { get; set; } = new List<T>();
    }
}
