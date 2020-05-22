using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

    public class TodoListsMenuViewModel : ViewModelBase
    {
        public interface ITodoListSelector
        {
            void OnListSelected(string name);
        }

        public TodoListsMenuViewModel(ITodoListSelector selector)
        {
            _selector = selector;
        }

        private ItemViewModel selectedList;
        private ITodoListSelector _selector;

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
