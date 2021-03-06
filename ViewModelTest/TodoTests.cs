using System;
using System.Linq;
using Todo;
using Todo.Persistance.Impl;
using ViewModels;
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

            Assert.Single(todo.Items); 
        }


        [Fact]
        public void TheUserCanAddMultipleItems()
        {
            var todo = new TodoList();
            todo.Add("a reminder");
            Assert.Single(todo.Items);

            //Add another item
            todo.Add("My second item");
            Assert.Equal(2, todo.Items.Count);
        }

        [Fact]
        public void TheUserCanCompleteITems()
        {
            var todo = new TodoList();
            var reminder = todo.Add("a reminder");

            Assert.Single(todo.Items);

            //The user can finish an item
            todo.Complete(reminder);

            var completedItems = todo.Items.Where(x => x.Completed).ToList();
            Assert.Single(completedItems);

        }

        [Fact]
        public void TheUserCanDeleteItems()
        {
            var todo = new TodoList();
            var item = todo.Add("a reminder");

            Assert.Single(todo.Items);

            //The user can finish an item
            todo.Remove(item);

            Assert.Empty(todo.Items);
        }
    }


    public class TodoSerializerTest
    {
        [Fact]
        public void SerializeingProducesTheCorrecSTrings()
        {
            //It saves completed items
            Assert.Equal("one,True", TodoItemSerializer.AsSeralizedString(new TodoItem("one") { Completed = true }));

            //And non completed ones
            Assert.Equal("two,False", TodoItemSerializer.AsSeralizedString(new TodoItem("two") ));
        }

        [Fact]
        public void ItCanDeserializeItems()
        {
            //It saves completed items
            var expectedItem = new TodoItem("one") { Completed = true };
            Assert.Equal(expectedItem, TodoItemDeserizliser.FromSerializedString("one,True"));

            //And non completed ones

            var expectedUncompleted = new TodoItem("two") { Completed = false };
            Assert.Equal(expectedUncompleted, TodoItemDeserizliser.FromSerializedString("two,False"));
        }
    }
}
