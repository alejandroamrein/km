using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Views;

namespace KinesiologiaMiramar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand _NuevoPacienteCommand;
        private ICommand _PacientesCommand;
        private ICommand _TurnosCommand;
        private ICommand _ReportesCommand;
        private ICommand _OrdenesCommand;
        private ICommand _ObrasSocialesCommand;
        private ICommand _SalirCommand;
        private double _Scale;
        private bool _IsDirty;

        public MainViewModel()
        {
            _Scale = 1;
        }

        public override string Title { get { return ""; } }
        public override bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
            }
        }

        public ICommand ObrasSocialesCommand
        {
            get
            {
                if (_ObrasSocialesCommand == null)
                {
                    _ObrasSocialesCommand = new RelayCommand((p) => {
                        var vm = new ObrasSocialesViewModel();
                        var uc = new ObrasSocialesUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _ObrasSocialesCommand;
            }
        }

        public ICommand NuevoPacienteCommand
        {
            get
            {
                if (_NuevoPacienteCommand == null)
                {
                    _NuevoPacienteCommand = new RelayCommand((p) => {
                        var vm = new NuevoPacienteViewModel();
                        var uc = new NuevoPacienteUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _NuevoPacienteCommand;
            }
        }

        public ICommand PacientesCommand
        {
            get
            {
                if (_PacientesCommand == null)
                {
                    _PacientesCommand = new RelayCommand((p) => {
                        var vm = new PacientesViewModel();
                        vm.SetMainContent += (sender, e) => { RaiseSetMainContent(e.UserControl); };
                        var uc = new PacientesUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _PacientesCommand;
            }
        }

        public ICommand SalirCommand
        {
            get
            {
                if (_SalirCommand == null)
                {
                    _SalirCommand = new RelayCommand((p) => {
                        RaiseClose();
                    });
                }
                return _SalirCommand;
            }
        }

        public ICommand ReportesCommand
        {
            get
            {
                if (_ReportesCommand == null)
                {
                    _ReportesCommand = new RelayCommand((p) => {
                        var vm = new ReportesViewModel();
                        var uc = new ReportesUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _ReportesCommand;
            }
        }

        public ICommand TurnosCommand
        {
            get
            {
                if (_TurnosCommand == null)
                {
                    _TurnosCommand = new RelayCommand((p) => {
                        var vm = new TurnosViewModel();
                        vm.SetMainContent += (sender, e) => { RaiseSetMainContent(e.UserControl); };
                        var uc = new TurnosUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _TurnosCommand;
            }
        }

        public ICommand OrdenesCommand
        {
            get
            {
                if (_OrdenesCommand == null)
                {
                    _OrdenesCommand = new RelayCommand((p) => {
                        var vm = new OrdenesViewModel();
                        vm.SetMainContent += (sender, e) => { RaiseSetMainContent(e.UserControl); };
                        var uc = new OrdenesUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _OrdenesCommand;
            }
        }

        public double Scale
        {
            get
            {
                return _Scale;
            }

            set
            {
                _Scale = value;
            }
        }
    }
}
