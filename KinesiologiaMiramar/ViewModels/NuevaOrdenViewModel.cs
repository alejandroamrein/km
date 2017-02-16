using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Models;

namespace KinesiologiaMiramar.ViewModels
{
    public class NuevaOrdenViewModel : ViewModelBase
    {
        private ICommand _SaveCommand;
        private bool _IsDirty;
        private Orden _Orden;
        private List<Paciente> _Pacientes;

        public NuevaOrdenViewModel()
        {
            _Orden = new Orden();
            _Orden.Fecha = DateTime.Today;
            _Orden.Sesiones = 10;
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

        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand((p) => {
                        if (_Orden.FK_Paciente == null)
                        {
                            ErrorMessage = "Falta seleccionar el paciente";
                        }
                        var entities = new kmEntities();
                        using (entities)
                        {
                            entities.Ordenes.Add(_Orden);
                            entities.SaveChanges();
                        }
                    });
                }
                return _SaveCommand;
            }
        }

        public Orden Orden
        {
            get
            {
                return _Orden;
            }
            set
            {
                _Orden = value;
                RaisePropertyChanged("Orden");
            }
        }

        public List<Paciente> Pacientes
        {
            get
            {
                if (_Pacientes == null)
                {
                    var entities = new kmEntities();
                    using (entities)
                    {
                        var q = from p in entities.Pacientes
                                orderby p.Apellido
                                select p;
                        _Pacientes = q.ToList();
                    }
                }
                return _Pacientes;
            }
        }
    }
}
