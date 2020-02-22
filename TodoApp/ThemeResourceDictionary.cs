using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TodoList
{
    public enum Themes
    {
        Light,
        Dark
    }

    public class ThemeResourceDictionary : ResourceDictionary
    {
        private Uri darkSource;
        private Uri lightSource;

        public void ReloadTheme()
        {
            UpdateTheme();
            LightSource =new Uri( "/Views;component/Styles/LightColors.xaml",UriKind.Relative);
            DarkSource = new Uri("/Views;component/Styles/DarkColors.xaml", UriKind.Relative);
        }

 
        public void UpdateTheme()
        {
            var val = App.CurrentTheme == Themes.Dark ? DarkSource : LightSource;
            if (val != null)
                base.Source = val; 
        }

        public Uri DarkSource
        {
            get => darkSource; 
            set
            {
                darkSource = value;
                UpdateTheme();
            }
        }
        public Uri LightSource
        {
            get => lightSource; 
            set
            {
                lightSource = value;
                UpdateTheme();
            }
        }

    }
}
