using System.ComponentModel.DataAnnotations;

namespace SomeonesToDoListApp.Models
{
    public class ToDoListRequest
    {
        [Range(1, 100)]
        public int PageSize { get; set; }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        public ToDoListRequest()
        {
        }

        public ToDoListRequest(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}