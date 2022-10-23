using SomeonesToDoListApp.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.Services.Interfaces
{
    public interface IToDoService
    {
        Task<bool> CreateToDoAsync(ToDoViewModel toDoViewModel);
        Task<IEnumerable<ToDoViewModel>> GetToDoItemsAsync();
    }
}