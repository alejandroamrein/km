using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Models;
using System.Data.Entity;

namespace KinesiologiaMiramar.ViewModels
{
    public class NuevoPacienteViewModel : ViewModelBase
    {
        private enum Modo
        {
            Edicion,
            Alta
        }
        private ICommand _SaveCommand;
        private bool _IsDirty;
        private Paciente _Paciente;
        private Modo _Modo;
        private List<TipoDeIva> _TiposDeIva;
        private List<ObraSocial> _ObrasSociales;

        public NuevoPacienteViewModel()
        {
            _Paciente = new Models.Paciente();
            _Paciente.Localidad = "Miramar";
            _Paciente.CodPostal = "7607";
            _Paciente.Sexo = "M";
            _Paciente.FecNac = DateTime.Today;
            _Paciente.FechaIngreso = DateTime.Today;
            _Modo = Modo.Alta;
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
                        try
                        {
                            var entities = new kmEntities();
                            using (entities)
                            {
                                if (_Modo == Modo.Alta)
                                {
                                    entities.Pacientes.Add(_Paciente);
                                }
                                else
                                {
                                    entities.Entry(_Paciente).State = EntityState.Modified;
                                }
                                entities.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    });
                }
                return _SaveCommand;
            }
        }

        public Paciente Paciente
        {
            get
            {
                return _Paciente;
            }
            set
            {
                _Paciente = value;
                // Si se seta de afuera no es alta sino edicion
                _Modo = Modo.Edicion;
                RaisePropertyChanged("Paciente");
            }
        }

        public List<TipoDeIva> TiposDeIva
        {
            get
            {
                if (_TiposDeIva == null)
                {
                    _TiposDeIva = TipoDeIva.Tipos.ToList();
                }
                return _TiposDeIva;
            }
        }

        public List<ObraSocial> ObrasSociales
        {
            get
            {
                if (_ObrasSociales == null)
                {
                    var db = new kmEntities();
                    using (db)
                    {
                        _ObrasSociales = db.ObrasSociales.OrderBy(os => os.Descripcion).ToList();
                    }
                }
                return _ObrasSociales;
            }
        }

        public DateTime? FecNac
        {
            get
            {
                return _Paciente.FecNac;
            }
            set
            {
                _Paciente.FecNac = value;
                RaisePropertyChanged("Edad");
            }
        }

        public string Edad
        {
            get
            {
                return _Paciente.EdadHoy;
            }
        }
    }
}
