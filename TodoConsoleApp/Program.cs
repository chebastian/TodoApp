using System;
using System.Linq;
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
            if(theCommand == Commands.List)
            {
                ListTodos(v[0]);
            }
            else if(theCommand == Commands.Add)
            {
                AddTodo(v[0], v[1]);
            }
            else if(theCommand == Commands.Complete)
            {
                CompleteItem(v[0], v[1]);
            }
        }

        private void CompleteItem(string v1, string v2)
        {
            var theItem = _vm.Items.Where(x => x.Name == v1).FirstOrDefault();
            _vm.CompleteItemCommand.Execute(theItem);
        }

        private void AddTodo(string todo, string file)
        {
            var writer = new TodoItemSerializer();
            _vm.LoadCommand.Execute(null);
            _vm.NextTodoName = todo;
            _vm.AddCommand.Execute(null);
            _vm.SaveCommand.Execute(null);
        }

        private async void ListTodos(string v)
        {
            _vm.LoadCommand.Execute(null);
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
            //Commands theCommand = ParseArgs(args);

            Commands theCommand = Commands.List;
            var listofArgs = new[] {"filename","something new","file.txt" };
            if(theCommand != Commands.Help)
            {
                app.Execute(theCommand,listofArgs.Skip(1).ToArray()); 
            }
 
            Console.WriteLine("Help: <TODO>");
        }

        private static Commands ParseArgs(string[] args)
        {
            if(args.Length > 0)
            {
                var fst = args[1];
                if (fst == "-list")
                    return Commands.List;
                else if (fst == "-add")
                    return Commands.Add;
                else if (fst == "-remove")
                    return Commands.Remove;
            }

            return Commands.Help;
        }
    }
}
