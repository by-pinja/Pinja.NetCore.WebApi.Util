using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Pinja.NetCore.WebApi.Util.OrderBy
{
    public class OrderByQueryString<T> : IValidatableObject
    {
        private static ImmutableDictionary<Type, IEnumerable<string>> s_reflectionCache = ImmutableDictionary<Type, IEnumerable<string>>.Empty;
        private static readonly object s_cacheLocker = new();

        private readonly string? _queryString;

        public OrderByQueryString()
        {
            _queryString = null;
        }

        // Query string is in form myfield / myfield,asc / myfield,desc
        public OrderByQueryString(string queryString)
        {
            _queryString = queryString;
        }

        public IQueryable<T> Query(IQueryable<T> query)
        {
            var (field, isDescending) = ParseQueryStringOrDefault() ?? throw new InvalidOperationException($"Could not parse query string {_queryString}");
            return query.OrderBy($"{field}{(isDescending ? " descending" : "")}");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            var parsedQuery = ParseQueryStringOrDefault();

            if (parsedQuery == default)
            {
                yield return new ValidationResult($"Could not parse orderBy query '{_queryString}'. Expected format 'field,order' -> 'myField,desc' or 'myField,asc'");
            }
            else if (SupportedFields().All(x => x != parsedQuery?.field))
            {
                yield return new ValidationResult($"Non supported orderBy field in '{_queryString}', supported fields: {string.Join(", ", SupportedFields())}");
            }
        }

        private (string field, bool descending)? ParseQueryStringOrDefault()
        {
            if (string.IsNullOrEmpty(_queryString))
                return (SupportedFields().First(), true);

            var tokens = _queryString.Split(",");

            if (tokens.Length == 1 || (tokens.Length == 2 && tokens[1] == "asc"))
                return (tokens[0], false);

            if (tokens.Length == 2 && tokens[1] == "desc")
                return (tokens[0], true);

            return default;
        }

        public IEnumerable<string> SupportedFields()
        {
            lock (s_cacheLocker)
            {
                if (!s_reflectionCache.ContainsKey(typeof(T)))
                {
                    var validProperties = typeof(T).GetProperties()
                        .Where(x => x.GetSetMethod() != null) // Filter only data properties and not for example "Projection" etc properties.
                        .Select(x => x.Name.Substring(0, 1).ToLower() + x.Name[1..])
                        .ToArray();

                    s_reflectionCache = s_reflectionCache.Add(typeof(T), validProperties);
                }

                return s_reflectionCache[typeof(T)];
            }
        }
    }
}
