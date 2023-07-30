using CleanArchitectureSample.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitectureSample.Infrastructure.Repository
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> TakePagination<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            int position = (pageIndex - 1) * pageSize;
            if (pageIndex < 1 || pageSize < 1)
            {
                position = 0;
            }
            return query.Skip(position).Take(pageSize);
        }
    }
}
