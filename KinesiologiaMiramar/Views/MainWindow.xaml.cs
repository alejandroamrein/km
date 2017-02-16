using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.ViewModels;

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _vm.Close += (sender, e) => { this.Close(); };
            _vm.SetMainContent += (sender, e) => {
                MainContent.Children.Clear();
                MainContent.Children.Add(e.UserControl);
            };
            DataContext = _vm;
        }

    }
}
