using System.Collections.Generic;
using System.Threading.Tasks;
using Todo;

namespace ViewModels
{
    public interface ITodoItemSaver
    {
        Task<bool> Save(List<TodoItem> items);
    }
}
