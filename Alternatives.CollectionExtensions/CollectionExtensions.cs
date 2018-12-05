using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alternatives.CollectionExtensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnumerable, Expression<Func<T, bool>> predicate = null)
        {
            bool result = iEnumerable == null ||
                          IsNullOrEmpty(iEnumerable.AsQueryable(), predicate);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IQueryable<T> iQueryable, Expression<Func<T, bool>> predicate = null)
        {
            bool result;

            if (iQueryable == null)
            {
                result = true;
            }
            else
            {
                result = predicate == null
                             ? !iQueryable.Any()
                             : !iQueryable.Any(predicate);
            }

            return result;
        }

        public static IList<IEnumerable<T>> GetAllSubSet<T>(this IList<T> source)
        {
            if (source == null)
                return new List<IEnumerable<T>>();
            if (!source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1).ToList();

            IEnumerable<T> element = source.Take(1);

            IList<IEnumerable<T>> haveNots = GetAllSubSet(source.Skip(1).ToList());
            IEnumerable<IEnumerable<T>> haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots).ToList();
        }
    }
}