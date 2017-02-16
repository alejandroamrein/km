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
using KinesiologiaMiramar.ViewModels;

namespace KinesiologiaMiramar.Views
{
    /// <summary>
    /// Interaction logic for ReportesUserControl.xaml
    /// </summary>
    public partial class ReportesUserControl : UserControl
    {
        private ReportesViewModel _vm;

        public ReportesUserControl(ReportesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}