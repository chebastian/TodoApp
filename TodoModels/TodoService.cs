﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Interfaces
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

    public class InMemoryTodoService : ITodoService
    {
        public InMemoryTodoService(string name)
        {
            Name = name;
            _list = new TodoList();
        }

        public string Name { get; }

        private TodoList _list;

        public Task<TodoItem> Add(TodoItem item)
        {
            return Task.FromResult(_list.Add(item.Name) );
        }

        public Task<TodoItem> Complete(TodoItem item)
        {
            return Task.FromResult(_list.Complete(item));
        }

        public Task<List<TodoItem>> ListTodos()
        {
            return Task.FromResult(_list.Items);
        }

        public  Task<bool> Remove(TodoItem item)
        {
            var wasRemoved = _list.Items.Contains(item);
            _list.Remove(item);
            return Task.FromResult(wasRemoved);
        }

        public Task<List<TodoItem>> Load(string name)
        {
            _list.Items = new List<TodoItem>() { new TodoItem(name + 1), new TodoItem(name + 2), new TodoItem(name + 3)   };
            return Task.FromResult(_list.Items);
        }

        public Task<bool> Save(string fileName)
        {
            return Task.FromResult(true);
        }
    }
}