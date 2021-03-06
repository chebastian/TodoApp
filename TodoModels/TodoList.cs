﻿using System.Collections.Generic;
using System.Linq;

namespace Todo
{

    public class TodoList
    {
        public TodoList()
        {
            Items = new List<TodoItem>();
        }

        public List<TodoItem> Items { get; set; }

        public TodoItem Add(string v)
        {
            var item = new TodoItem(v);
            Items.Add(item);
            return item;
        }

        public TodoItem Complete(TodoItem todo)
        {
            var theItem = Items.FirstOrDefault(x => x.Name == todo.Name);
            if(theItem != null)
            {
                theItem.Completed = true;
            }

            return new TodoItem(todo.Name) { Completed = true };
        }

        public void Remove(TodoItem item)
        {
            Items.Remove(item);
        }
    }
}