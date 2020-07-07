namespace Todo.Persistance
{
    using System;
    using System.Collections.Generic;
    using Todo;

    public interface ITodoLoader
    {
        IAsyncEnumerable<TodoItem> Items();
        IEnumerable<(Guid, string)> Lists();
    }
}
