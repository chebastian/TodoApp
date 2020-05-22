using Sefe.Utils.MVVM;
using System.Collections.ObjectModel;
using ViewModels.TodoMenu;

namespace ViewModels
{
    public class TodoPageViewModel : ViewModelBase, TodoListsMenuViewModel.ITodoListSelector
    {
        private TodoListViewModel _listViewModel;

        public TodoPageViewModel()
        {
            ListViewModel = new TodoListViewModel();
            MenuViewModel = new TodoListsMenuViewModel(this)
            {
                Lists = new ObservableCollection<TodoMenu.TodoListViewModel>()
                {
                    new TodoMenu.TodoListViewModel(){Name="saved.txt"},
                    new TodoMenu.TodoListViewModel(){Name="another.txt"},
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
