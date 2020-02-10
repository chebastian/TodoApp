using System;
using System.Text;

namespace ViewModels
{
    using System.IO;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Todo;

    public interface ITodoLoader
    {
        IAsyncEnumerable<TodoItem> Items();
    }

    public class TodoItemSerializer
    {
        public static string AsSeralizedString(TodoItem item)
        {
            return $"{item.Name},{item.Completed}";
        }
    }

    public class TodoItemDeserizliser
    {
        public static TodoItem FromSerializedString(string item)
        {
            var split = item.Split(',');
            return new TodoItem(split[0]) { Completed = bool.Parse(split[1]) };
        }
    }

    public class TodoItemSaver : TodoListViewModel.ITodoItemSaver
    {
        private string _path;

        public TodoItemSaver(string filename)
        {
            _path = filename;
        }

        public Task<bool> Save(List<TodoItem> items)
        {
            return Task.Run(() =>
            {

                var lines = new List<string>();
                foreach (var item in items)
                {
                    lines.Add(TodoItemSerializer.AsSeralizedString(item));
                }

                try
                {
                    File.WriteAllLines(_path, lines);
                }
                catch
                {
                    return false;
                }

                return true;
            });
        }
    }

    public class TodoLoader : ITodoLoader
    {
        public TodoLoader(string path) => Location = path;

        public string Location { get; }

        public async IAsyncEnumerable<TodoItem> Items()
        {
            if (File.Exists(Location))
            {
                var lines = File.ReadAllLines(Location);
                foreach (var line in lines)
                {
                    yield return TodoItemDeserizliser.FromSerializedString(line);
                }
            }

            //yield return new TodoItem("First");
            //await Task.Delay(200);
            //yield return new TodoItem("Second");
            //await Task.Delay(200);
            //yield return new TodoItem("Third");
            //yield return new TodoItem("a");
            //yield return new TodoItem("b");
            //yield return new TodoItem("c half second to next...");
            //await Task.Delay(500);
            //yield return new TodoItem("Fource Long wait 2 sec to last ");
            //await Task.Delay(2000);
            //yield return new TodoItem("Last one");

        }
    }
}
