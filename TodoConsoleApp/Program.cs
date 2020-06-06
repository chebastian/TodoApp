using System;
using System.Linq;
using System.Threading.Tasks;
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

        public App()
        {
            _vm = new TodoListViewModel();
            _writer = new MyConsoleWriter();
        }

        internal void Execute(Commands theCommand, string[] v)
        {
            if (theCommand == Commands.List)
            {
                ListTodos(v[1]);
            }
            else
            {
                if (theCommand == Commands.Add)
                {
                    AddTodo(v[1], v[2]);
                }
                else if (theCommand == Commands.Complete)
                {
                    CompleteItem(v[1], v[2]);
                }

                _vm.SaveCommand.Execute(null);
                ListTodos(v[0]);
            }

        }

        private void CompleteItem(string v1, string v2)
        {
            _vm.LoadCommand.Execute(null);

            var theItem = _vm.Items.Where(x => x.Name == v1).FirstOrDefault();
            _vm.CompleteItemCommand.Execute(theItem);
        }

        private void AddTodo(string todo, string file)
        {
            _vm.LoadCommand.Execute(null);
            _vm.NextTodoName = todo;
            _vm.AddCommand.Execute(null);
            _vm.SaveCommand.Execute(null);
        }

        private void ListTodos(string v)
        {
            _vm.LoadCommand.Execute(null);
            while (!_vm.LoadIsReady)
            {
            }

            foreach (var item in _vm.Items)
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
