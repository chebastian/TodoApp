using Sefe.Utils.MVVM;
using System.Collections.ObjectModel;
using Todo.Persistance.Impl;
using Todo.Service.Interface.Impl;
using ViewModels.TodoMenu;

namespace ViewModels
{
    public class TodoPageViewModel : ViewModelBase, TodoListsMenuViewModel.ITodoListSelector
    {
        private TodoListViewModel _listViewModel;

        public TodoPageViewModel()
        {
            var todoService = new ComponentService("", x => new TodoItemSaver(x), x => new TodoLoader(x));
            ListViewModel = new TodoListViewModel(todoService);
            MenuViewModel = new TodoListsMenuViewModel(this)
            {
                Lists = new ObservableCollection<TodoMenu.ItemViewModel>()
                {
                    new TodoMenu.ItemViewModel(){Name="saved.txt"},
                    new TodoMenu.ItemViewModel(){Name="another.txt"},
                }
            };
        }

        public TodoListViewModel ListViewModel
        {
            get
            {
                return _listViewModel;
            }

            set
            {
                _listViewModel = value;
                OnPropertyChanged();
            }
        }

        public TodoListsMenuViewModel MenuViewModel { get; set; }

        public void OnListSelected(string name)
        {
            ListViewModel.SwitchList(name);
        }
    }
}
