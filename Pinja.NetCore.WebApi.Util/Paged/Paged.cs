using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pinja.NetCore.WebApi.Util.Paged
{
    public class Paged<T>
    {
        [JsonConstructor]
        public Paged() { }

        protected Paged(IEnumerable<T> data, int resultCount, int pageCount, int pageSize)
        {
            Data = data;
            ResultCount = resultCount;
            PageCount = pageCount;
            PageSize = pageSize;
        }

        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int ResultCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public static Paged<T> FromQuery(IQueryable<T> data, int page, int pageSize = 50)
        {
            var result = data.Skip(page * pageSize).Take(pageSize + 1).ToList();

            var dataCount = data.Count();

            var pages = (dataCount + pageSize - 1) / pageSize;

            return new Paged<T>(result.Take(pageSize), dataCount, pages, pageSize);
        }
    }
}
