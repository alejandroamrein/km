using KinesiologiaMiramar.ViewModels;
using KinesiologiaMiramar.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace KinesiologiaMiramar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _MainViewModel;

        public MainViewModel MainViewModel
        {
            get
            {
                return _MainViewModel;
            }

            set
            {
                _MainViewModel = value;
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeCultures();
            MainViewModel = new MainViewModel();
            var w = new MainWindow(MainViewModel);
            this.MainWindow = w;
            w.Show();
        }

        private void InitializeCultures()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ar");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ar");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
