using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Themes CurrentTheme { get; set; } = Themes.Light;
        protected override void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);
        }
    }
}
