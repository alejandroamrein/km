using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Helpers;

namespace KinesiologiaMiramar.ViewModels
{
    public class ReportesViewModel : ViewModelBase
    {
        private ICommand _SalirCommand;
        private ICommand _EjecutarCommand;
        private List<KeyValuePair<int, string>> _Reportes;
        private int? _SelectedReporte;
        private bool _IsDirty;

        public ReportesViewModel()
        {
            _Reportes = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Listado de pacientes"),
                new KeyValuePair<int, string>(2, "Listado de turnos"),
                new KeyValuePair<int, string>(3, "Listado de sesiones realizadas")
            };
        }

        public List<KeyValuePair<int, string>> Reportes
        {
            get
            {
                return _Reportes;
            }
        }

        public override string Title { get { return "Reportes"; } }
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

        public ICommand EjecutarCommand
        {
            get
            {
                if (_SalirCommand == null)
                {
                    _SalirCommand = new RelayCommand((p) => {
                        MessageBox.Show("Ejecutar reporte " + _SelectedReporte);
                    });
                }
                return _SalirCommand;
            }
        }

        public int? SelectedReporte
        {
            get
            {
                return _SelectedReporte;
            }
            set
            {
                _SelectedReporte = value;
                RaisePropertyChanged("SelectedReporte");
                ChangeParameterControl();
            }
        }

        private void ChangeParameterControl()
        {
            // Sacar control actual
            // Poner control en Grid
        }
    }
}
