using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Todo;

namespace ViewModels
{
    public class TodoItemViewModel : ViewModelBase
    {
        private bool completed;
        private TodoItem item;

        public TodoItemViewModel(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item
        {
            get => item; set
            {
                item = value;
                OnPropertyChanged(nameof(Completed));
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool Completed
        {
            get => Item.Completed;
        }

        public string Name
        {
            get => Item.Name;
        }
    }
    public class TodoListViewModel : ViewModelBase
    {
        private string nextTodoName;

        public TodoListViewModel()
        {
            AddCommand = new RelayCommand(OnAdd);
            RemoveCommand = new RelayCommand(OnRemove);
            CompleteItemCommand = new RelayCommand(OnCompleteItem);
            Todo = new Todo.TodoList();
            Items = new ObservableCollection<TodoItemViewModel>();
        }

        private void OnCompleteItem(object obj)
        {
            Todo.Complete((obj as TodoItemViewModel).Item);

            var todolist = Todo.Items;
            foreach (var item in Items)
            {
                todolist.Contains(item.Item);
                item.Item = todolist.First(x => x.Name == item.Name);
            }
            OnPropertyChanged(nameof(Items));
        }

        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand CompleteItemCommand { get; set; }
        public TodoList Todo { get; }

        public string NextTodoName
        {
            get => nextTodoName; set
            {
                nextTodoName = value;
                OnPropertyChanged();
            }
        }

        private void OnRemove(object obj)
        {
            Todo.Remove((obj as TodoItemViewModel).Item);

            var todolist = Todo.Items;
            var toRemove = new List<TodoItemViewModel>();
            foreach (var item in Items)
            {
                if (todolist.Contains(item.Item))
                {
                    item.Item = todolist.First(x => x.Name == item.Name);
                }
                else
                {
                    toRemove.Add(item);
                }
            }

            foreach (var item in toRemove)
                Items.Remove(item);

            OnPropertyChanged(nameof(Items));
        }

        private void OnAdd(object obj)
        {
            Todo.Add(NextTodoName);
            Items = new ObservableCollection<TodoItemViewModel>(
                Todo.Items.Select(x => new TodoItemViewModel(x)).ToList()
                );

            OnPropertyChanged(nameof(Items));
            NextTodoName = "";
        }
    }
}
