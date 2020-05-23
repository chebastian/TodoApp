using System;
using System.Text;

namespace Todo.Persistance.Impl
{
    using System.IO;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Todo;
    using Todo.Persistance;

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

    public class TodoItemSaver : ITodoItemSaver
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

        }
    }
}
