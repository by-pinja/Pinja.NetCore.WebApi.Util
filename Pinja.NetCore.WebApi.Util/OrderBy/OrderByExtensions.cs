using System.Linq;

namespace Pinja.NetCore.WebApi.Util.OrderBy
{
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, OrderByQueryString<T> queryDescription)
        {
            return queryDescription.Query(query);
        }
    }
}
