using System.Collections.Generic;
using System.Threading.Tasks;
using Todo;

namespace Todo.Service.Interface
{
    public interface ITodoService
    {
        Task<List<TodoItem>> ListTodos();
        Task<TodoItem> Add(TodoItem item);
        Task<TodoItem> Complete(TodoItem item);
        Task<bool> Remove(TodoItem item);

        Task<List<TodoItem>> Load(string fileName);
        Task<bool> Save(string fileName);
    }
}
