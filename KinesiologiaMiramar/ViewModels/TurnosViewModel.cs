using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Views;
using KinesiologiaMiramar.Models;
using System.Data.Entity;
using System.Windows.Media;
using System.Timers;

namespace KinesiologiaMiramar.ViewModels
{
    public class TurnosViewModel : ViewModelBase
    {
        private ICommand _PacientesCommand;
        private ICommand _TurnosCommand;
        private ICommand _SalirCommand;
        private ICommand _PreviaCommand;
        private ICommand _SiguienteCommand;
        private ICommand _SeleccionarOrdenCommand;
        private ICommand _VerImagenCommand;
        private WeekViewModel _WeekViewModel;
        private Orden _Orden;
        private Brush _IndicatorBackground;
        private readonly Brush _TransparentBrush = new SolidColorBrush(Colors.Transparent);
        private readonly Brush _HiliteBrush = new SolidColorBrush(Colors.Yellow);
        private Timer _HiliteTimer;

        public TurnosViewModel()
        {
            _HiliteTimer = new Timer();
            _HiliteTimer.Enabled = false;
            _HiliteTimer.Elapsed += (sender, e) => {
                IndicatorBackground = _TransparentBrush;
                (sender as Timer).Enabled = false;
            };
            _HiliteTimer.Interval = 5000;
            _IndicatorBackground = _TransparentBrush;
            var hoy = DateTime.Today;
            _WeekViewModel = new WeekViewModel(SemanaDe(hoy), (dia, turnoHora, slot) => {
                if (_Orden == null)
                {
                    MessageBox.Show("Seleccionar una orden primero");
                    return null;
                }
                // TODO: decidir de que semana estamos hablando y pasar esa semana como parametro 
                //       para que el constructor de WeekViewModel arme los slots segun la semana
                // Dia es el dia (esta claro)
                // turno es el numero de turno: 
                //     1 -> 09:00 - 09:30
                //     2 -> 09:30 - 10:00
                //     3 -> 10:00 - 10:30
                //     4 -> 10:30 - 11:00
                //     5 -> 11:00 - 11:30
                //     6 -> 17:00 - 17:30
                //     7 -> 17:30 - 18:00
                //     ...
                //    12 -> 20:00 - 20:30
                //    13 -> 20:30 - 21:00
                // slot es 1 a 5 (por turno se pueden asignar 5 slots)
                return CrearTurno(dia, turnoHora, slot, _Orden);
            });
            _WeekViewModel.Cut += (sender, e) => { MessageBox.Show("Cut " + e.Turno.Orden.Paciente.ApellidoNombre); };
            _WeekViewModel.Copy += (sender, e) => { MessageBox.Show("Copy " + e.Turno.Orden.Paciente.ApellidoNombre); };
            _WeekViewModel.Paste += (sender, e) => { MessageBox.Show(string.Format("Paste {0} {1} {2}", e.Dia, e.TurnoHora, e.Slot)); };
            _WeekViewModel.Delete += (sender, e) => {
                var db = new kmEntities();
                using (db)
                {
                    var turno = db.Turnos.Find(e.Turno.PK_Turno);
                    if (_Orden != null && turno.FK_Orden.Value == _Orden.PK_Orden)
                    {
                        int usadas = UpdateSesionesUsadas(turno.FK_Orden.Value);
                        _Orden.SesionesUsadas = usadas - 1;
                        RaisePropertyChanged("Restantes");
                    }
                    db.Turnos.Remove(turno);
                    db.SaveChanges();
                }
            };
        }

        private Turno CrearTurno(DateTime dia, int turnoHora, int slot, Orden _Orden)
        {
            var db = new kmEntities();
            using (db)
            {
                var turno = new Turno()
                {
                    Fecha = dia,
                    TurnoHora = turnoHora,
                    Slot = slot,
                    FK_Orden = _Orden.PK_Orden,
                    Presente = false
                };
                db.Turnos.Add(turno);
                db.SaveChanges();
                var t = from x in db.Turnos.Include("Orden").Include("Orden.Paciente")
                        where x.PK_Turno == turno.PK_Turno
                        select x;

                if (_Orden != null && turno.FK_Orden.Value == _Orden.PK_Orden)
                {
                    int usadas = UpdateSesionesUsadas(turno.FK_Orden.Value);
                    _Orden.SesionesUsadas = usadas;
                    RaisePropertyChanged("Restantes");
                }

                return t.First();
            }
        }

        private int UpdateSesionesUsadas(int fK_Orden)
        {
            int usadas = 0;
            var db = new kmEntities();
            using (db)
            {
                var turnosAsignados = db.Turnos.Where(t => t.FK_Orden == fK_Orden).Count();
                var orden = db.Ordenes.Find(fK_Orden);
                orden.SesionesUsadas = turnosAsignados;
                usadas = turnosAsignados;
                db.SaveChanges();
            }
            IndicatorBackground = _HiliteBrush;
            _HiliteTimer.Enabled = true;
            return usadas;
        }

        private DateTime SemanaDe(DateTime hoy)
        {
            DateTime lunes = hoy;
            while (lunes.DayOfWeek != DayOfWeek.Monday)
            {
                lunes = lunes.AddDays(-1);
            }
            return lunes;
        }

        public WeekViewModel WeekViewModel
        {
            get
            {
                return _WeekViewModel;
            }
            set
            {
                _WeekViewModel = value;
                RaisePropertyChanged("WeekViewModel");
            }
        }

        private bool _IsDirty;

        public override string Title { get { return "Turnos"; } }
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

        public ICommand SeleccionarOrdenCommand
        {
            get
            {
                if (_SeleccionarOrdenCommand == null)
                {
                    _SeleccionarOrdenCommand = new RelayCommand((p) =>
                    {
                        var uc = new OrdenesUserControl();
                        RaiseSetMainContent(uc);
                    });
                }
                return _SeleccionarOrdenCommand;
            }
        }

        public ICommand PacientesCommand
        {
            get
            {
                if (_PacientesCommand == null)
                {
                    _PacientesCommand = new RelayCommand((p) =>
                    {
                        MessageBox.Show("Pacientes");
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
                    _SalirCommand = new RelayCommand((p) =>
                    {
                        RaiseClose();
                    });
                }
                return _SalirCommand;
            }
        }

        public ICommand TurnosCommand
        {
            get
            {
                if (_TurnosCommand == null)
                {
                    _TurnosCommand = new RelayCommand((p) =>
                    {
                        MessageBox.Show("Turnos");
                    });
                }
                return _TurnosCommand;
            }
        }

        public ICommand PreviaCommand
        {
            get
            {
                if (_PreviaCommand == null)
                {
                    _PreviaCommand = new RelayCommand((p) =>
                    {
                        WeekViewModel.Lunes = WeekViewModel.Lunes.AddDays(-7);
                    });
                }
                return _PreviaCommand;
            }
        }

        public ICommand SiguienteCommand
        {
            get
            {
                if (_SiguienteCommand == null)
                {
                    _SiguienteCommand = new RelayCommand((p) =>
                    {
                        WeekViewModel.Lunes = WeekViewModel.Lunes.AddDays(7);
                    });
                }
                return _SiguienteCommand;
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

        public ICommand VerImagenCommand
        {
            get
            {
                if (_VerImagenCommand == null)
                {
                    _VerImagenCommand = new RelayCommand((p) =>
                    {
                        var vm = new OrdenImagenViewModel(_Orden);
                        var win = new OrdenImagenWindow(vm);
                        win.Show();
                    });
                }
                return _VerImagenCommand;
            }
        }

        public Visibility OrdenInfoVisibility
        {
            get
            {
                return _Orden == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Brush IndicatorBackground
        {
            get
            {
                return _IndicatorBackground;
            }

            set
            {
                _IndicatorBackground = value;
                RaisePropertyChanged("IndicatorBackground");
            }
        }

        public int Restantes
        {
            get
            {
                return _Orden.SesionesRestantes;
            }
        }
    }
}
