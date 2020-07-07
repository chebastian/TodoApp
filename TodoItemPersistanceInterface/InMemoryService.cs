using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Todo;
using Todo.Persistance;

namespace Todo.Service.Interface.Impl
{
    public class TodoService : ITodoService
    {
        private ITodoItemSaver Saver => _saverFactory(_name);
        private ITodoLoader Loader => _loaderFactory(_name);
        public TodoList _list;
        private string _name;
        private Func<string, ITodoItemSaver> _saverFactory;
        private Func<string, ITodoLoader> _loaderFactory;

        public TodoService(string name,Func<string,ITodoItemSaver> saver, Func<string,ITodoLoader> loader)
        {
            _name = name;
            _saverFactory = saver;
            _loaderFactory = loader;
            _list = new TodoList(); 
        } 

        public Task<TodoItem> Add(TodoItem item)
        {
            return Task.FromResult(_list.Add(item.Name));
        }

        public Task<TodoItem> Complete(TodoItem item)
        {
            return Task.FromResult(_list.Complete(item));
        }

        public async Task<List<TodoItem>> ListTodos()
        {
            var res =  new List<TodoItem>();
            await foreach(var item in Loader.Items())
            {
                res.Add(item);
            }
            return res;
        }

        public Task<bool> Remove(TodoItem item)
        {
            var wasRemoved = _list.Items.Contains(item);
            _list.Remove(item);
            return Task.FromResult(wasRemoved);
        }

        public async Task<List<TodoItem>> Load(string name)
        {
            _name = name;
            _list.Items.Clear();
            await foreach(var item in Loader.Items())
            {
                _list.Items.Add(item);
            }


            return _list.Items;
        }

        public async Task<bool> Save(string fileName)
        {
            _name = fileName;
            var res = await Task.Run(() => Saver.Save(_list.Items));
            return true;
        }

        public Task<IEnumerable<(Guid, string)>> GetLists()
        {
            return Task.FromResult(Loader.Lists());
        }
    }

    public class InMemoryTodoService : ITodoService
    {
        public InMemoryTodoService(string name)
        {
            Name = name;
            _list = new TodoList();
            _list.Items = new List<TodoItem>() { new TodoItem(name + 1), new TodoItem(name + 2), new TodoItem(name + 3) };
        }

        public string Name { get; }

        private TodoList _list;

        public Task<TodoItem> Add(TodoItem item)
        {
            return Task.FromResult(_list.Add(item.Name));
        }

        public Task<TodoItem> Complete(TodoItem item)
        {
            return Task.FromResult(_list.Complete(item));
        }

        public Task<List<TodoItem>> ListTodos()
        {
            return Task.FromResult(_list.Items);
        }

        public Task<bool> Remove(TodoItem item)
        {
            var wasRemoved = _list.Items.Contains(item);
            _list.Remove(item);
            return Task.FromResult(wasRemoved);
        }

        public Task<List<TodoItem>> Load(string name)
        {
            _list.Items = new List<TodoItem>() { new TodoItem(name + 1), new TodoItem(name + 2), new TodoItem(name + 3) };
            return Task.FromResult(_list.Items);
        }

        public Task<bool> Save(string fileName)
        {
            return Task.FromResult(true);
        }

        public Task<IEnumerable<(Guid, string)>> GetLists()
        {
            throw new NotImplementedException();
        }
    }
}

