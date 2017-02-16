using System.Windows.Controls;
using KinesiologiaMiramar.ViewModels;
using System.Windows;
using KinesiologiaMiramar.Models;

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for PacientesUserControl.xaml
    /// </summary>
    public partial class PacientesUserControl : UserControl
    {
        private PacientesViewModel _vm;

        public PacientesUserControl(PacientesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
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

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as Paciente;
            _vm.EditarOrdenCommand.Execute(obj.PK_Paciente);
        }
    }
}
