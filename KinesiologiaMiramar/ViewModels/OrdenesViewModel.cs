using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Models;
using KinesiologiaMiramar.Helpers;
using System.Collections.ObjectModel;
using KinesiologiaMiramar.Views;
using System.ComponentModel;
using System;

namespace KinesiologiaMiramar.ViewModels
{
    public class OrdenesViewModel : ViewModelBase
    {
        private ICommand _AgregarOrdenCommand;
        private ICommand _ImprimirCommand;
        private ICommand _AsignarTurnoCommand;
        private ICommand _VerImagenCommand;
        private ICommand _ClearPacienteCommand;
        private List<Orden> _Ordenes;
        private List<Paciente> _Pacientes;
        private int? _SelectedPaciente;
        private bool _OnlyOrdenesAbiertas;
        private string _Texto;
        private bool _IsDirty;

        public OrdenesViewModel()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                //in Design mode
                _Ordenes = new List<Orden>();
                _Pacientes = new List<Paciente>();
            }
            else
            {
                var db = new kmEntities();
                using (db)
                {
                    LoadOrdenes();
                    var q = db.Pacientes.OrderBy(p => p.Apellido);
                    _Pacientes = new List<Paciente>(q.ToList());
                }
            }
        }

        public override string Title { get { return "Ordenes"; } }
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

        public ICommand ClearPacienteCommand
        {
            get
            {
                if (_ClearPacienteCommand == null)
                {
                    _ClearPacienteCommand = new RelayCommand((p) => {
                        SelectedPaciente = null;
                    });
                }
                return _ClearPacienteCommand;
            }
        }

        private void LoadOrdenes()
        {
            var entities = new kmEntities();
            using (entities)
            {
                IQueryable<Orden> q = entities.Ordenes.Include("Paciente");
                if (_OnlyOrdenesAbiertas)
                {
                    q = q.Where(o => o.Sesiones - o.SesionesUsadas > 0);
                }
                if (_SelectedPaciente != null)
                {
                    q = q.Where(o => o.FK_Paciente == _SelectedPaciente);
                }
                if (!string.IsNullOrEmpty(_Texto))
                {
                    q = q.Where(x => x.Paciente.Documento.Contains(_Texto) || x.Medico.Contains(_Texto) || x.Paciente.Apellido.Contains(_Texto) || x.Paciente.Nombre.Contains(_Texto));
                }
                Ordenes = new List<Orden>(q.ToList());
            }
            RaisePropertyChanged("NroOrdenes");
        }

        public ICommand AgregarOrdenCommand
        {
            get
            {
                if (_AgregarOrdenCommand == null)
                {
                    _AgregarOrdenCommand = new RelayCommand((p) => {
                        var vm = new NuevaOrdenViewModel();
                        var uc = new NuevaOrdenUserControl(vm);
                        RaiseSetMainContent(uc);
                    });
                }
                return _AgregarOrdenCommand;
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

        public List<Orden> Ordenes
        {
            get
            {
                return _Ordenes;
            }

            set
            {
                _Ordenes = value;
                RaisePropertyChanged("Ordenes");
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
                LoadOrdenes();
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
                LoadOrdenes();
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
                LoadOrdenes();
            }
        }

        public int MaxOrdenes
        {
            get
            {
                var db = new kmEntities();
                using (db)
                {
                    return db.Ordenes.Count();
                }
            }
        }

        public int NroOrdenes
        {
            get
            {
                return _Ordenes.Count();
            }
        }
    }
}
