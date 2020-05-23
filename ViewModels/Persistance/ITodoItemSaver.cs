using System.Collections.Generic;
using System.Threading.Tasks;
using Todo;

namespace Todo.Persistance
{
    public interface ITodoItemSaver
    {
        Task<bool> Save(List<TodoItem> items);
    }
}
