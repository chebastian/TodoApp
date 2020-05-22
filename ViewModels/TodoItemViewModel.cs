using Sefe.Utils.MVVM;
using Todo;

namespace ViewModels
{
    public class TodoItemViewModel : ViewModelBase
    {
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
}
