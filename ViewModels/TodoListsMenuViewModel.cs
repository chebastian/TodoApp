using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Todo.Service.Interface;

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

    public class FlyoutViewModel : ViewModelBase
    {
        public virtual void OnOpen() { }
    }

    public class NewTodoViewModel : FlyoutViewModel
    {

    }

    public class SaveViewModel : FlyoutViewModel
    {
        public SaveViewModel(ITodoService service)
        {
            SaveCommand = new RelayCommand(() => service.Save(SaveName)); 
        }

        private string _saveName;
        public string SaveName
        {
            get { return _saveName; }
            set { _saveName = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; set; }

    }

    public class LoadViewModel : FlyoutViewModel
    {
        private ITodoService _service;
        private TodoListsMenuViewModel.ITodoListSelector _selector;
        private ItemViewModel selectedList;

        public LoadViewModel(ITodoService service, TodoListsMenuViewModel.ITodoListSelector selector)
        {
            _service = service;
            _selector = selector;
        }

        public override void OnOpen()
        {
            var items = _service.GetLists().Result.Select(x => new ItemViewModel() { Name = x.name }).ToList();
            Items = new ObservableCollection<ItemViewModel>(items);
            OnPropertyChanged(nameof(Items));
        }

        public ObservableCollection<ItemViewModel> Items { get; set; }
        public ItemViewModel SelectedList
        {
            get => selectedList;
            set
            {
                //TODO this should never happen or be handled in the service
                if (value == null)
                    return;

                selectedList = value;
                _selector.OnListSelected(value.Name);
                OnPropertyChanged();
            }
        }
    }

    public class TodoListsMenuViewModel : ViewModelBase
    {
        public interface ITodoListSelector
        {
            void OnListSelected(string name);
        }

        public TodoListsMenuViewModel(ITodoListSelector selector, ITodoService service)
        {
            _service = service;
            _selector = selector;
            MenuItemClickedCommand = new RelayCommand(OnMenuClicked);
        }

        private void OnMenuClicked(object obj)
        {
            Current = obj as FlyoutViewModel;
            Current.OnOpen();
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
        public SaveViewModel Save => new SaveViewModel(_service);
        public LoadViewModel Load { get { return new LoadViewModel(_service, _selector); } }
        public FlyoutViewModel Current { get; set; } = new FlyoutViewModel();


        private ItemViewModel selectedList;
        private ITodoService _service;
        private ITodoListSelector _selector;

        public ICommand MenuItemClickedCommand { get; set; }

        public ObservableCollection<ItemViewModel> Lists { get; set; }

    }
}
