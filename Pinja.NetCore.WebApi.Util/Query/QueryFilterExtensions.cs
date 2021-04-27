using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pinja.NetCore.WebApi.Util.OrderBy;

namespace Pinja.NetCore.WebApi.Util.Query
{
    public static class QueryFilterExtensions
    {
        public static IQueryable<T> FilterWhen<T>(this IQueryable<T> source, Func<bool> predicate, Expression<Func<T, bool>> filter)
        {
            if (predicate.Invoke())
            {
                source = source.Where(filter);
            }

            return source;
        }

        public static IQueryable<T> FilterWhen<T>(this IQueryable<T> source, string? filterString, Expression<Func<T, bool>> filter)
        {
            if (!string.IsNullOrEmpty(filterString))
            {
                source = source.Where(filter);
            }

            return source;
        }

        public static IQueryable<T> FilterWhen<T>(this IQueryable<T> source, string[] filterStrings, Expression<Func<T, bool>> filter)
        {
            if (filterStrings != default && filterStrings.Length > 0)
            {
                source = source.Where(filter);
            }

            return source;
        }

        public static IQueryable<T> FilterWhen<T>(this IQueryable<T> source, ICollection<Guid>? guids, Expression<Func<T, bool>> filter)
        {
            if (guids != null && guids.Count > 0)
            {
                source = source.Where(filter);
            }

            return source;
        }

        public static IQueryable<T> SearchWhen<T>(this IQueryable<T> source, string? filterString, params Expression<Func<T, bool>>[] filters)
        {
            var combinedOrOperations = filters.Aggregate((a, b) => a.Or(b));

            if (!string.IsNullOrEmpty(filterString))
            {
                source = source.Where(combinedOrOperations);
            }

            return source;
        }
    }
}
