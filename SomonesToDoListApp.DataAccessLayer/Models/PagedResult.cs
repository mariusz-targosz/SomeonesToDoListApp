using System.Collections.Generic;

namespace SomeonesToDoListApp.DataAccessLayer.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; }
        public IEnumerable<T> Items { get; }

        public PagedResult(int totalCount, IEnumerable<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}