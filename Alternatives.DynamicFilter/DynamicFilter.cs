using System.Collections.Generic;
using System.Linq;

namespace Alternatives.DynamicFilter
{
    public class DynamicFilter
    {
        private readonly List<DynamicFilterItem> _dynamicFilterItems = new List<DynamicFilterItem>();

        private IReadOnlyList<DynamicFilterItem> DynamicFilterItems => _dynamicFilterItems;

        public void AddFilter(DynamicFilterItem dynamicFilterItem)
        {
            _dynamicFilterItems.Add(dynamicFilterItem);
        }

        public void RemoveFilter(DynamicFilterItem dynamicFilterItem)
        {
            _dynamicFilterItems.Remove(dynamicFilterItem);
        }

        public IQueryable<T> Filter<T>(IQueryable<T> queryable)
        {
            foreach (DynamicFilterItem dynamicFilterItem in DynamicFilterItems)
            {
                queryable = dynamicFilterItem.Apply<T>(queryable);
            }

            return queryable;
        }
    }

}