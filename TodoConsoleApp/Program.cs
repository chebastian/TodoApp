using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Persistance.Impl;
using Todo.Service.Interface;
using Todo.Service.Interface.Impl;
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
        private IUiWriter _writer;


        private ITodoService _todo;

        public App()
        {
            _writer = new MyConsoleWriter();
        }

        internal async Task Execute(Commands theCommand, string[] v)
        {
            if (theCommand == Commands.List)
            {
                var theList = v[1];
                _todo = new TodoService(theList,x => new TodoItemSaver(x), x => new TodoLoader(x));
                await ListTodos(theList);
            }
            else
            {
                var theList = v[2];
                _todo = new TodoService(theList,x => new TodoItemSaver(x), x => new TodoLoader(x));
                var theItem = v[1];
                await _todo.Load(theList);
                if (theCommand == Commands.Add)
                {
                    await AddTodo(theItem, theList);
                }
                else if (theCommand == Commands.Complete)
                {
                    await CompleteItem(theItem,theList);
                }

                await _todo.Save(theList);
                await ListTodos(theList);
            }

        }

        private async Task CompleteItem(string item,string file)
        {
            await _todo.Complete(new Todo.TodoItem(item));
        }

        private async Task AddTodo(string todo, string file)
        {
            await _todo.Add(new Todo.TodoItem(todo));
        }

        private async Task ListTodos(string v)
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

        static async Task Main(string[] args)
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
                await app.Execute(theCommand, listofArgs.ToArray());
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
