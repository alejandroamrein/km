using System;
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
using KinesiologiaMiramar.ViewModels;
using KinesiologiaMiramar.Models;

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for OrdenesUserControl.xaml
    /// </summary>
    public partial class OrdenesUserControl : UserControl
    {
        private OrdenesViewModel _vm;

        public OrdenesUserControl(OrdenesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _vm.SetMainContent += (sender, e) => { (Application.Current as App).MainViewModel.RaiseSetMainContent(e.UserControl); };
            DataContext = _vm;
        }

        public OrdenesUserControl()
        {
            InitializeComponent();
            _vm = new ViewModels.OrdenesViewModel();
            _vm.SetMainContent += (sender, e) => { (Application.Current as App).MainViewModel.RaiseSetMainContent(e.UserControl); };
            DataContext = _vm;
        }

        private void VerImagenButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as Orden;
            _vm.VerImagenCommand.Execute(obj.PK_Orden);
        }

        private void AsignarTurnoButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as Orden;
            _vm.AsignarTurnoCommand.Execute(obj.PK_Orden);
        }
    }
}
