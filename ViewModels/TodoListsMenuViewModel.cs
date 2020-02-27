using Sefe.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModels.TodoMenu
{
    public class TodoListViewModel : ViewModelBase
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
        public ObservableCollection<TodoListViewModel> Lists { get; set; }
    }
}
