﻿namespace Todo.Persistance
{
    using System.Collections.Generic;
    using Todo;

    public interface ITodoLoader
    {
        IAsyncEnumerable<TodoItem> Items();
    }
}
