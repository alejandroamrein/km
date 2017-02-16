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
using Microsoft.Win32;

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for NuevaOrdenUserControl.xaml
    /// </summary>
    public partial class NuevaOrdenUserControl : UserControl
    {
        private NuevaOrdenViewModel _vm;

        public NuevaOrdenUserControl(NuevaOrdenViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        private void BuscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            var dr = dlg.ShowDialog();
            if (dr.HasValue && dr.Value)
            {
                var bmp = new BitmapImage(new Uri(dlg.FileName));
                img1.Source = bmp;
            }
        }
    }
}
