using System.Linq.Expressions;

namespace AnaliticTrend.Application.Utilities
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> queryAble,
            bool condition,
            Expression<Func<T, bool>> predicate
            )
        {
            if (condition)
            {
                return queryAble.Where(predicate);
            }
            return queryAble;
        }
    }
}
