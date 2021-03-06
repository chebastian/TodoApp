﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TodoPage.DataContext = new TodoPageViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentTheme == Themes.Dark)
                App.CurrentTheme = Themes.Light;
            else
                App.CurrentTheme = Themes.Dark;

            App.CurrentLanguage = Lang.Eng;

            Resources.Clear();
            Resources.MergedDictionaries.Clear();

            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri( "Shared.xaml",UriKind.Relative )}); 
        }
    }
}
