using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private bool removed;

        public bool Removed
        {
            get { return removed; }
            set { removed = value; OnPropertyChanged(); }
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
        public interface ITodoItemSaver
        {
            Task<bool> Save(List<TodoItem> items);
        }

        private string nextTodoName;

        public TodoListViewModel()
        {
            AddCommand = new RelayCommand(OnAdd);
            RemoveCommand = new RelayCommand(OnRemove);
            CompleteItemCommand = new RelayCommand(OnCompleteItem);
            PreRemoveCommand = new RelayCommand(OnPreRemove);
            Todo = new Todo.TodoList();
            Items = new ObservableCollection<TodoItemViewModel>();

            SerializeLocation = "./saved.txt";
            SaveCommand = new RelayCommand(OnSave);
            LoadCommand = new RelayCommand(OnLoad);
        }

        private void OnPreRemove(object obj)
        {
            ToRemove = obj as TodoItemViewModel;
        }

        private async void OnSave(object obj)
        {
            var saver = new TodoItemSaver(SerializeLocation);
            await saver.Save(Todo.Items);
        }

        private async void OnLoad(object obj)
        {
            var loader = new TodoLoader(SerializeLocation);
            Todo.Items.Clear();
            Items.Clear();
            await foreach (var item in loader.Items())
            {
                Todo.Items.Add(item);
                Items.Add(new TodoItemViewModel(item));
            }
        }

        private void OnCompleteItem(object obj)
        {
            (obj as TodoItemViewModel).Item = Todo.Complete((obj as TodoItemViewModel).Item);
        }

        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand PreRemoveCommand { get; set; }
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

        public TodoItemViewModel ToRemove { get; private set; }
        public string SerializeLocation { get; private set; }

        private void OnRemove(object obj)
        {
            Todo.Remove((obj as TodoItemViewModel).Item);
            Items.Remove((obj as TodoItemViewModel));
        }

        private void OnAdd(object obj)
        {
            var added = Todo.Add(NextTodoName);

            var newItem = new TodoItemViewModel(added);
            Items.Add(newItem);

            OnPropertyChanged(nameof(Items));
            NextTodoName = "";
        }
    }
}
