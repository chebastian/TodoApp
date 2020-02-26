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
using ViewModels;

namespace Views
{
    /// <summary>
    /// Interaction logic for TodoItemView.xaml
    /// </summary>
    public partial class TodoItemView : UserControl
    {
        public TodoItemView()
        {
            InitializeComponent();
        }
        private void RemoveCompleted(object sender, EventArgs e)
        {
            RemovedCommand.Execute(DataContext);
        }



        public ICommand RemovedCommand
        {
            get { return (ICommand)GetValue(RemovedCommandProperty); }
            set { SetValue(RemovedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemovedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemovedCommandProperty =
            DependencyProperty.Register("RemovedCommand", typeof(ICommand), typeof(TodoItemView), new FrameworkPropertyMetadata(Changed));

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
