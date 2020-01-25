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
    /// Interaction logic for TodoPage.xaml
    /// </summary>
    public partial class TodoPage : UserControl
    {
        public TodoPage()
        {
            InitializeComponent();
        }

        private void FocusOnText(object sender, RoutedEventArgs e)
        {
            nameBox.Focus();
        }
    }
}
