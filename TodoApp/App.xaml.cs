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
    public enum Lang
    {
        Sv,
        Eng
    }

    public partial class App : Application
    {
        private static Lang currentLanguage = Lang.Sv;

        public static Themes CurrentTheme { get; set; } = Themes.Light;
        public static Lang CurrentLanguage
        {
            get => currentLanguage;
            set
            {
                currentLanguage = value;
                var res = value switch
                {
                    Lang.Sv => new System.Globalization.CultureInfo("en-SE"),
                    _ => new System.Globalization.CultureInfo("en-US")
                };

                System.Threading.Thread.CurrentThread.CurrentUICulture = res;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            CurrentLanguage = Lang.Eng;

            base.OnStartup(e);
        }
    }
}
