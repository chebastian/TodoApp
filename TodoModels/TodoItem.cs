using System;

namespace Todo
{
    public class TodoItem
    {
        public TodoItem(string name)
        {
            Name = name;
            Completed = false;
        }

        public string Name { get; set; }
        public bool Completed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TodoItem item &&
                   Name == item.Name &&
                   Completed == item.Completed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Completed);
        }
    }
}