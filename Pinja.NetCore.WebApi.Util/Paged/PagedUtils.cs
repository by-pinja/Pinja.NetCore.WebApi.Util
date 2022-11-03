using System.Linq;

namespace Pinja.NetCore.WebApi.Util.Paged
{
    public static class PagedUtils
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> data, int page, int pageSize = 50)
        {
            return data.Skip(page * pageSize).Take(pageSize);
        }
    }
}
