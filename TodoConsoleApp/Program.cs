using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Interfaces;
using Todo.Persistance.Impl;
using ViewModels;

namespace TodoConsoleApp
{
    public interface IUiWriter
    {
        void Print(string text);
        void PrintLn(string text);
    }

    public class MyConsoleWriter : IUiWriter
    {
        public void Print(string text)
        {
            Console.Write(text);
        }

        public void PrintLn(string text)
        {
            Console.WriteLine(text);
        }
    }

    public enum Commands
    {
        Help,
        List,
        Add,
        Complete,
        Remove,
    };

    public class App
    {
        private TodoListViewModel _vm;
        private IUiWriter _writer;
        private ITodoService _todo;

        public App()
        {
            _vm = new TodoListViewModel();
            _writer = new MyConsoleWriter();
            _todo = new InMemoryTodoService("test");
        }

        internal void Execute(Commands theCommand, string[] v)
        {
            if (theCommand == Commands.List)
            {
                var theList = v[1];
                ListTodos(theList);
            }
            else
            {
                var theItem = v[1];
                var theList = v[2];
                if (theCommand == Commands.Add)
                {
                    AddTodo(theItem, theList);
                }
                else if (theCommand == Commands.Complete)
                {
                    CompleteItem(theItem);
                }

                _todo.Save(theList);
                ListTodos(theList);
            }

        }

        private void CompleteItem(string item)
        {
            _todo.Complete(new Todo.TodoItem(item));
        }

        private async void AddTodo(string todo, string file)
        {
            await _todo.Load(file);
            await _todo.Add(new Todo.TodoItem(todo));
        }

        private async void ListTodos(string v)
        {
            var items = await _todo.ListTodos();
            foreach (var item in items)
            {
                var checkmark = item.Completed ? "[x]" : "[ ]";
                _writer.PrintLn($"{checkmark}\t{item.Name}");
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var app = new App();

            //var listofArgs = args;
            var testFile = "saved.txt";
            //var listofArgs = new[] { "-list", testFile };
            var listofArgs = new[] { "-add", "newItem", testFile };
            //var listofArgs = new[] { "-complete", "test", testFile };
            //var listofArgs = new[] { "-remove", "theItem", testFile };
            //var listofArgs = new[] { "-complete", "one", testFile };

            Commands theCommand = ParseArgs(listofArgs);

            if (theCommand != Commands.Help)
            {
                app.Execute(theCommand, listofArgs.ToArray());
            }

            Console.WriteLine("Help: <TODO>");
        }

        private static Commands ParseArgs(string[] args)
        {
            if (args.Length > 0)
            {
                var fst = args[0];
                if (fst == "-list")
                    return Commands.List;
                else if (fst == "-add")
                    return Commands.Add;
                else if (fst == "-remove")
                    return Commands.Remove;
                else if (fst == "-complete")
                    return Commands.Complete;
            }

            return Commands.Help;
        }
    }
}
