using Sefe.Utils.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Todo;

namespace ViewModels
{

    public class TodoListViewModel : ViewModelBase
    {

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
            FileIsReady = true;
            LoadIsReady = true;
        }


        public void SwitchList(string name)
        {
            SerializeLocation = name;
            LoadCommand.Execute(null);
        }

        private void OnPreRemove(object obj)
        {
            ToRemove = obj as TodoItemViewModel;
        }

        private async void OnSave(object obj)
        {
            FileIsReady = false;
            var saver = new TodoItemSaver(SerializeLocation);
            await saver.Save(Todo.Items);
            FileIsReady = true;

        }

        private async void OnLoad(object obj)
        {
            LoadIsReady = false;
            while (!FileIsReady)
            {
                await Task.Delay(20);
            }

            var loader = new TodoLoader(SerializeLocation);
            Todo.Items.Clear();
            Items.Clear();
            await foreach (var item in loader.Items())
            {
                Todo.Items.Add(item);
                Items.Add(new TodoItemViewModel(item));
            }

            LoadIsReady = true;
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
        public bool FileIsReady { get; private set; }
        public bool LoadIsReady { get; private set; }

        private void OnRemove(object obj)
        {
            Todo.Remove((obj as TodoItemViewModel).Item);
            Items.Remove((obj as TodoItemViewModel));
        }

        private void OnAdd(object obj)
        {
            if (NextTodoName.StartsWith(":n") && NextTodoName.Length > 1)
            {
                CreateNewList(string.Join("", NextTodoName.Skip(2)));
            }
            else if (NextTodoName.StartsWith(":w") && NextTodoName.Length > 1)
            {
                OnSave(null);
            }
            else if (NextTodoName.StartsWith(":") && NextTodoName.Length > 1)
            {
                SwitchList(string.Join("", NextTodoName.Skip(1)));
            }
            else
            {
                var added = Todo.Add(NextTodoName);

                var newItem = new TodoItemViewModel(added);
                Items.Add(newItem);

            }

            OnPropertyChanged(nameof(Items));
            NextTodoName = "";
        }

        private void CreateNewList(string v)
        {
            Todo.Items.Clear();
            Items.Clear();
            SerializeLocation = v.Trim();
            OnSave(null);
        }
    }
}
