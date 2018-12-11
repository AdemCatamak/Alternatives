using System.Linq;

namespace Alternatives.DynamicFilter.Operator
{
    public abstract class Operator
    {
        public abstract IQueryable<T> Apply<T>(IQueryable<T> queryable, string propertyName, object value);
    }
}