using System.Collections.Generic;

namespace SomeonesToDoListApp.Models
{
    public class ToDoListResponse
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public int TotalCount { get; }
        public IReadOnlyList<ToDoResponse> Items { get; }

        public ToDoListResponse(int pageSize, int pageNumber, int totalCount, IReadOnlyList<ToDoResponse> items)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            Items = items;
        }
    }
}