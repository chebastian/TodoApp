using System;
using System.Linq;
using Todo;
using Xunit;

namespace ViewModelTest
{
    public class TodoTests
    {
        [Fact]
        public void TheUserCanAddItemsToTheirTodo()
        {
            var todo = new TodoList();
            todo.Add("a reminder");

            Assert.Equal(todo.Items.Count, 1); 
        }


        [Fact]
        public void TheUserCanAddMultipleItems()
        {
            var todo = new TodoList();
            todo.Add("a reminder");
            Assert.Equal(todo.Items.Count, 1);

            //Add another item
            todo.Add("My second item");
            Assert.Equal(todo.Items.Count, 2);
        }

        [Fact]
        public void TheUserCanCompleteITems()
        {
            var todo = new TodoList();
            var reminder = todo.Add("a reminder");

            Assert.Equal(todo.Items.Count, 1);

            //The user can finish an item
            todo.Complete(reminder);

            var completedItems = todo.Items.Where(x => x.Completed).ToList();
            Assert.Equal(completedItems.Count, 1); 

        }

        [Fact]
        public void TheUserCanDeleteItems()
        {
            var todo = new TodoList();
            var item = todo.Add("a reminder");

            Assert.Equal(todo.Items.Count, 1);

            //The user can finish an item
            todo.Remove(item);

            Assert.Equal(todo.Items.Count, 0); 
        }
    }
}
