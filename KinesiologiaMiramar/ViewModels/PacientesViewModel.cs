using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Models;
using KinesiologiaMiramar.Helpers;
using System.Collections.ObjectModel;
using KinesiologiaMiramar.Views;
using System;

namespace KinesiologiaMiramar.ViewModels
{
    public class PacientesViewModel : ViewModelBase
    {
        private ICommand _AgregarPacienteCommand;
        private ICommand _ImprimirCommand;
        private ICommand _BuscarCommand;
        private ICommand _AsignarTurnoCommand;
        private ICommand _VerImagenCommand;
        private ICommand _EditarOrdenCommand;
        private ICommand _ClearOsCommand;
        private List<Paciente> _Pacientes;
        private List<ObraSocial> _ObrasSociales;
        private List<Orden> _Ordenes;
        private int? _SelectedPaciente = null;
        private string _Texto;
        private bool _IsDirty;
        private int? _SelectedOs;
        private bool _OnlyOrdenesAbiertas;

        public PacientesViewModel()
        {
            var entities = new kmEntities();
            using (entities)
            {
                var q = entities.ObrasSociales;
                _ObrasSociales = new List<ObraSocial>(q.ToList());
                LoadPacientes();
            }
            _SelectedPaciente = null;
        }

        private void LoadPacientes()
        {
            var entities = new kmEntities();
            using (entities)
            {
                IQueryable<Paciente> q = entities.Pacientes;
                if (_OnlyOrdenesAbiertas)
                {
                    q = q.Where(p => p.Orden.Where(o => o.Sesiones - o.SesionesUsadas > 0).Any());
                }
                if (_SelectedOs != null)
                {
                    q = q.Where(p => p.FK_Os == _SelectedOs);
                }
                if (!string.IsNullOrEmpty(_Texto))
                {
                    q = q.Where(x => x.Documento.Contains(_Texto) || x.Nombre.Contains(_Texto) || x.Apellido.Contains(_Texto) || x.Calle.Contains(_Texto));
                }

                Pacientes = new List<Paciente>(q.ToList());
            }
            RaisePropertyChanged("NroPacientes");
        }

        public override string Title { get { return "Pacientes"; } }
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

        public ICommand BuscarCommand
        {
            get
            {
                if (_BuscarCommand == null)
                {
                    _BuscarCommand = new RelayCommand((p) => {
                        LoadPacientes();
                    });
                }
                return _BuscarCommand;
            }
        }

        public ICommand ClearOsCommand
        {
            get
            {
                if (_ClearOsCommand == null)
                {
                    _ClearOsCommand = new RelayCommand((p) => {
                        SelectedOs = null;
                    });
                }
                return _ClearOsCommand;
            }
        }

        public ICommand AgregarPacienteCommand
        {
            get
            {
                if (_AgregarPacienteCommand == null)
                {
                    _AgregarPacienteCommand = new RelayCommand((p) => {
                        var vm = new NuevoPacienteViewModel();
                        var uc = new NuevoPacienteUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _AgregarPacienteCommand;
            }
        }

        public ICommand ImprimirCommand
        {
            get
            {
                if (_ImprimirCommand == null)
                {
                    _ImprimirCommand = new RelayCommand((p) => {
                        MessageBox.Show("Imprimir");
                    });
                }
                return _ImprimirCommand;
            }
        }

        public List<Paciente> Pacientes
        {
            get
            {
                return _Pacientes;
            }

            set
            {
                _Pacientes = value;
                RaisePropertyChanged("Pacientes");
            }
        }

        public List<ObraSocial> ObrasSociales
        {
            get
            {
                return _ObrasSociales;
            }

            set
            {
                _ObrasSociales = value;
                RaisePropertyChanged("ObrasSociales");
            }
        }

        public string Texto
        {
            get
            {
                return _Texto;
            }

            set
            {
                _Texto = value;
                RaisePropertyChanged("Texto");
                LoadPacientes();
            }
        }

        public List<Orden> Ordenes
        {
            get
            {
                if (_Ordenes == null)
                {
                    _Ordenes = new List<Orden>();
                }
                return _Ordenes;
            }
        }

        public int? SelectedPaciente
        {
            get
            {
                return _SelectedPaciente;
            }

            set
            {
                _SelectedPaciente = value;
                RaisePropertyChanged("SelectedPaciente");
                _Ordenes = new List<Orden>();
                if (_SelectedPaciente.HasValue)
                {
                    var db = new kmEntities();
                    using (db)
                    {
                        _Ordenes = db.Ordenes.Where(o => o.FK_Paciente == _SelectedPaciente).ToList();
                    }
                }
                RaisePropertyChanged("Ordenes");
                RaisePropertyChanged("OrdenesHeight");
            }
        }

        public ICommand VerImagenCommand
        {
            get
            {
                if (_VerImagenCommand == null)
                {
                    _VerImagenCommand = new RelayCommand((p) => {
                        var vm = new OrdenImagenViewModel(_Ordenes.Where(o => o.PK_Orden == (int)p).First());
                        var win = new OrdenImagenWindow(vm);
                        win.Show();
                    });
                }
                return _VerImagenCommand;
            }
        }

        public ICommand EditarOrdenCommand
        {
            get
            {
                if (_EditarOrdenCommand == null)
                {
                    _EditarOrdenCommand = new RelayCommand((p) => {
                        var vm = new NuevoPacienteViewModel();
                        vm.SetMainContent += (sender, e) => { (Application.Current as App).MainViewModel.RaiseSetMainContent(e.UserControl); };
                        using (var db = new kmEntities())
                        {
                            var paciente = db.Pacientes.Find(p);
                            vm.Paciente = paciente;
                        }
                        var uc = new NuevoPacienteUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _EditarOrdenCommand;
            }
        }

        public ICommand AsignarTurnoCommand
        {
            get
            {
                if (_AsignarTurnoCommand == null)
                {
                    _AsignarTurnoCommand = new RelayCommand((p) => {
                        var vm = new TurnosViewModel();
                        vm.SetMainContent += (sender, e) => { (Application.Current as App).MainViewModel.RaiseSetMainContent(e.UserControl); };
                        using (var db = new kmEntities())
                        {
                            var orden = db.Ordenes.Find(p);
                            var paciente = orden.Paciente;
                            vm.Orden = orden;
                        }
                        var uc = new TurnosUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _AsignarTurnoCommand;
            }
        }

        public GridLength OrdenesHeight
        {
            get
            {
                return _SelectedPaciente == null || _Ordenes.Count == 0 ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
            }
        }

        public int? SelectedOs
        {
            get
            {
                return _SelectedOs;
            }
            set
            {
                _SelectedOs = value;
                RaisePropertyChanged("SelectedOs");
                LoadPacientes();
            }
        }

        public bool OnlyOrdenesAbiertas
        {
            get
            {
                return _OnlyOrdenesAbiertas;
            }
            set
            {
                _OnlyOrdenesAbiertas = value;
                RaisePropertyChanged("OnlyOrdenesAbiertas");
                LoadPacientes();
            }
        }

        public int MaxPacientes
        {
            get
            {
                var db = new kmEntities();
                using (db)
                {
                    return db.Pacientes.Count();
                }
            }
        }

        public int NroPacientes
        {
            get
            {
                return _Pacientes.Count();
            }
        }
    }
}
