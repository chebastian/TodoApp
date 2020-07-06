using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Views
{ 
    /// <summary>
    /// Interaction logic for TodoListsMenuView.xaml
    /// </summary>
    public partial class TodoListsMenuView : UserControl
    {
        public TodoListsMenuView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopupMenu.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopupMenu.IsOpen = !PopupMenu.IsOpen;
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopupMenu.IsOpen = false;
        }

        private void PopupMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            PopupMenu.IsOpen = false;
        }

        private void ToggleButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopupMenu.PlacementTarget = (sender as UIElement);
        }
    }
}
