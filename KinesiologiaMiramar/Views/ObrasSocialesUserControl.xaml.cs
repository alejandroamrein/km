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

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for ObrasSocialesUserControl.xaml
    /// </summary>
    public partial class ObrasSocialesUserControl : UserControl
    {
        private ObrasSocialesViewModel _vm;

        public ObrasSocialesUserControl(ObrasSocialesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}
