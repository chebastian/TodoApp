using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ViewModels.TodoMenu
{
    public class ItemViewModel : ViewModelBase
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
    }

    public class NewTodoViewModel : ViewModelBase
    {

    }

    public class SaveViewModel : ViewModelBase
    {

    }

    public class LoadViewModel : ViewModelBase
    {

    }

    public class TodoListsMenuViewModel : ViewModelBase
    {
        public interface ITodoListSelector
        {
            void OnListSelected(string name);
        }

        public TodoListsMenuViewModel(ITodoListSelector selector)
        {
            _selector = selector;
            MenuItemClickedCommand = new RelayCommand(OnMenuClicked);
        }

        private void OnMenuClicked(object obj)
        {
            Current = obj as ViewModelBase;
            OnPropertyChanged(nameof(Current));
            ShowPopup = false;
            ShowPopup = true;
        }

        private bool _showPopup;

        public bool ShowPopup
        {
            get { return _showPopup; }
            set { _showPopup = value; OnPropertyChanged(); }
        }

        public NewTodoViewModel New { get; set; } = new NewTodoViewModel();
        public SaveViewModel Save { get; set; } = new SaveViewModel();
        public LoadViewModel Load { get; set; } = new LoadViewModel();
        public ViewModelBase Current { get; set; } = new ViewModelBase();


        private ItemViewModel selectedList;
        private ITodoListSelector _selector;

        public ICommand  MenuItemClickedCommand { get; set; }

        public ObservableCollection<ItemViewModel> Lists { get; set; }

        public ItemViewModel SelectedList
        {
            get => selectedList;
            set
            {
                selectedList = value;
                _selector.OnListSelected(value.Name);
                OnPropertyChanged();
            }
        }
    }
}
