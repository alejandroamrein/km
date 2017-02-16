using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinesiologiaMiramar.ViewModels
{
    public class WeekViewModel : ViewModelBase
    {
        private SlotViewModel[][] _WeekData;
        private DateTime _Lunes;
        private Func<DateTime, int, int, Turno> _OrdenCallback;
        public event EventHandler<CutCopyEventArgs> Cut;
        public event EventHandler<CutCopyEventArgs> Copy;
        public event EventHandler<PasteEventArgs> Paste;
        public event EventHandler<CutCopyEventArgs> Delete;

        // TODO: si no recibo la semana que tengo que mostrar no se como armar los slots 
        public WeekViewModel(DateTime lunes, Func<DateTime, int, int, Turno> cb)
        {
            _Lunes = lunes;
            _OrdenCallback = cb;
            LoadWeekData();
        }

        public double Scale
        {
            get
            {
                return (Application.Current as App).MainViewModel.Scale;
            }
            set
            {
                (Application.Current as App).MainViewModel.Scale = value;
                RaisePropertyChanged("Scale");
            }
        }

        private void RaiseCutEvent(Turno turno)
        {
            if (Cut != null)
            {
                Cut(this, new ViewModels.CutCopyEventArgs(turno));
            }
        }

        private void RaiseCopyEvent(Turno turno)
        {
            if (Copy != null)
            {
                Copy(this, new ViewModels.CutCopyEventArgs(turno));
            }
        }

        private void RaiseDeleteEvent(Turno turno)
        {
            if (Delete != null)
            {
                Delete(this, new ViewModels.CutCopyEventArgs(turno));
            }
        }

        private void RaisePasteEvent(DateTime dia, int turnoHora, int slot)
        {
            if (Copy != null)
            {
                Paste(this, new ViewModels.PasteEventArgs(dia, turnoHora, slot));
            }
        }

        private void LoadWeekData()
        {
            _WeekData = new SlotViewModel[5][];
            for (var i = 0; i < 5; i++)
            {
                _WeekData[i] = new SlotViewModel[13];
                for (var j = 0; j < 13; j++)
                {
                    _WeekData[i][j] = new SlotViewModel(_Lunes.AddDays(i), j + 1, _OrdenCallback) { Turno1 = null, Turno2 = null, Turno3 = null, Turno4 = null, Turno5 = null };
                    _WeekData[i][j].Cut += (sender, e) => { RaiseCutEvent(e.Turno); };
                    _WeekData[i][j].Copy += (sender, e) => { RaiseCopyEvent(e.Turno); };
                    _WeekData[i][j].Paste += (sender, e) => { RaisePasteEvent(e.Dia, e.TurnoHora, e.Slot); };
                    _WeekData[i][j].Delete += (sender, e) => { RaiseDeleteEvent(e.Turno); };
                }
            }

            var db = new kmEntities();
            using (db)
            {
                var viernes = _Lunes.AddDays(4);
                var q = from x in db.Turnos
                        where x.Fecha >= _Lunes && x.Fecha <= viernes
                        select x;
                foreach (var x in q)
                {
                    var paciente = x.Orden.Paciente;
                    int dia = x.Fecha.Value.Subtract(_Lunes).Days;
                    switch (x.Slot)
                    {
                        case 1:
                            _WeekData[dia][x.TurnoHora.Value - 1].Turno1 = x;
                            break;                                
                        case 2:                                   
                            _WeekData[dia][x.TurnoHora.Value - 1].Turno2 = x;
                            break;                                
                        case 3:                                   
                            _WeekData[dia][x.TurnoHora.Value - 1].Turno3 = x;
                            break;                                
                        case 4:                                   
                            _WeekData[dia][x.TurnoHora.Value - 1].Turno4 = x;
                            break;                                
                        case 5:                                   
                            _WeekData[dia][x.TurnoHora.Value - 1].Turno5 = x;
                            break;
                        default:
                            break;
                    }
                }
            }
            RaisePropertyChanged("WeekData");
        }

        public SlotViewModel[][] WeekData
        {
            get
            {
                return _WeekData;
            }

            set
            {
                _WeekData = value;
                RaisePropertyChanged("WeekData");
            }
        }

        public override string Title
        {
            get
            {
                return "Week";
            }
        }

        public override bool IsDirty
        {
            get
            {
                return false;
            }

            set
            {
            }
        }

        public DateTime Lunes
        {
            get
            {
                return _Lunes;
            }

            set
            {
                _Lunes = value;
                LoadWeekData();
                RaisePropertyChanged("Lunes");
            }
        }
    }
}
