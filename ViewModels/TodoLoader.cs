using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo;

namespace ViewModels
{
    public interface ITodoLoader
    {
        IAsyncEnumerable<TodoItem> Items();
    }

    public class TodoItemSaver : TodoListViewModel.ITodoItemSaver
    {
        public Task<bool> Save(List<TodoItem> items)
        {
            return new Task<bool>(() => false);
        }
    }

    public class TodoLoader : ITodoLoader
    {
        public async IAsyncEnumerable<TodoItem> Items()
        {

            yield return new TodoItem("First");
            await Task.Delay(200);
            yield return new TodoItem("Second");
            await Task.Delay(200);
            yield return new TodoItem("Third");
            yield return new TodoItem("a");
            yield return new TodoItem("b");
            yield return new TodoItem("c half second to next...");
            await Task.Delay(500);
            yield return new TodoItem("Fource Long wait 2 sec to last "); 
            await Task.Delay(2000);
            yield return new TodoItem("Last one"); 

        }
    }
}
