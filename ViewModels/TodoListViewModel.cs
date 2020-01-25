﻿using Sefe.Utils.MVVM;
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
            (obj as TodoItemViewModel).Item = Todo.Complete((obj as TodoItemViewModel).Item);
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
            Items.Remove((obj as TodoItemViewModel));

            //OnPropertyChanged(nameof(Items));
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
