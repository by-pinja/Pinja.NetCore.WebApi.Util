using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pinja.NetCore.WebApi.Util.Paged
{
    public class PagedAggregate<T, TAggregate> : Paged<T>
    {
        [JsonConstructor]
        internal PagedAggregate() { }

        private PagedAggregate(IEnumerable<T> data, int resultCount, int pageCount, int pageSize, TAggregate aggregate) : base(data, resultCount, pageCount, pageSize)
        {
            Aggregates = aggregate;
        }

        public TAggregate Aggregates { get; set; } = default!;

        public static PagedAggregate<T, TAggregate> FromQuery(IQueryable<T> data, int page, TAggregate aggregates, int pageSize = 50)
        {
            var result = data.Skip(page * pageSize).Take(pageSize + 1).ToList();

            var dataCount = data.Count();

            var pages = (dataCount + pageSize - 1) / pageSize;

            return new PagedAggregate<T, TAggregate>(result.Take(pageSize), dataCount, pages, pageSize, aggregates);
        }
    }
}
