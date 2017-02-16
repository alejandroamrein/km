using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KinesiologiaMiramar.ViewModels
{
    public class SlotViewModel : ViewModelBase
    {
        private Turno[] _Turnos;
        private ICommand[] _ClickCommand;
        private ICommand[] _CutCommand;
        private ICommand[] _CopyCommand;
        private ICommand[] _DeleteCommand;
        private ICommand[] _PasteCommand;
        private Func<DateTime, int, int, Turno> _OrdenCallback;
        private DateTime _Dia;
        private int _TurnoHora;
        public event EventHandler<CutCopyEventArgs> Cut;
        public event EventHandler<CutCopyEventArgs> Copy;
        public event EventHandler<CutCopyEventArgs> Delete;
        public event EventHandler<PasteEventArgs> Paste;

        public SlotViewModel(DateTime dia, int turnoHora, Func<DateTime, int, int, Turno> cb)
        {
            _Turnos = new Turno[5];
            _Dia = dia;
            _TurnoHora = turnoHora;
            _OrdenCallback = cb;
            _ClickCommand = new RelayCommand[5];
            _CopyCommand = new RelayCommand[5];
            _CutCommand = new RelayCommand[5];
            _DeleteCommand = new RelayCommand[5];
            _PasteCommand = new RelayCommand[5];
        }

        public Turno Turno1
        {
            get
            {
                return _Turnos[0];
            }

            set
            {
                _Turnos[0] = value;
                RaisePropertyChanged("Turno1");
            }
        }
        public Turno Turno2
        {
            get
            {
                return _Turnos[1];
            }

            set
            {
                _Turnos[1] = value;
                RaisePropertyChanged("Turno2");
            }
        }
        public Turno Turno3
        {
            get
            {
                return _Turnos[2];
            }

            set
            {
                _Turnos[2] = value;
                RaisePropertyChanged("Turno3");
            }
        }
        public Turno Turno4
        {
            get
            {
                return _Turnos[3];
            }

            set
            {
                _Turnos[3] = value;
                RaisePropertyChanged("Turno4");
            }
        }
        public Turno Turno5
        {
            get
            {
                return _Turnos[4];
            }

            set
            {
                _Turnos[4] = value;
                RaisePropertyChanged("Turno5");
            }
        }

        public string Text1
        {
            get
            {
                if (_Turnos[0] == null)
                {
                    return "Libre";
                }
                return _Turnos[0].Orden.Paciente.ApellidoNombre;
            }
        }
        public string Text2
        {
            get
            {
                if (_Turnos[1] == null)
                {
                    return "Libre";
                }
                return _Turnos[1].Orden.Paciente.ApellidoNombre;
            }
        }
        public string Text3
        {
            get
            {
                if (_Turnos[2] == null)
                {
                    return "Libre";
                }
                return _Turnos[2].Orden.Paciente.ApellidoNombre;
            }
        }
        public string Text4
        {
            get
            {
                if (_Turnos[3] == null)
                {
                    return "Libre";
                }
                return _Turnos[3].Orden.Paciente.ApellidoNombre;
            }
        }
        public string Text5
        {
            get
            {
                if (_Turnos[4] == null)
                {
                    return "Libre";
                }
                return _Turnos[4].Orden.Paciente.ApellidoNombre;
            }
        }

        public ICommand Paste1Command
        {
            get
            {
                if (_PasteCommand[0] == null)
                {
                    _PasteCommand[0] = new RelayCommand((p) => {
                        OnPaste(1);
                    });
                }
                return _PasteCommand[0];
            }
        }
        public ICommand Paste2Command
        {
            get
            {
                if (_PasteCommand[1] == null)
                {
                    _PasteCommand[1] = new RelayCommand((p) => {
                        OnPaste(2);
                    });
                }
                return _PasteCommand[1];
            }
        }
        public ICommand Paste3Command
        {
            get
            {
                if (_PasteCommand[2] == null)
                {
                    _PasteCommand[2] = new RelayCommand((p) => {
                        OnPaste(3);
                    });
                }
                return _PasteCommand[2];
            }
        }
        public ICommand Paste4Command
        {
            get
            {
                if (_PasteCommand[3] == null)
                {
                    _PasteCommand[3] = new RelayCommand((p) => {
                        OnPaste(4);
                    });
                }
                return _PasteCommand[3];
            }
        }
        public ICommand Paste5Command
        {
            get
            {
                if (_PasteCommand[4] == null)
                {
                    _PasteCommand[4] = new RelayCommand((p) => {
                        OnPaste(5);
                    });
                }
                return _PasteCommand[4];
            }
        }
        private void OnPaste(int index)
        {
            if (Paste != null)
            {
                Paste(this, new PasteEventArgs(_Dia, _TurnoHora, index));
            }
        }

        public ICommand Copy1Command
        {
            get
            {
                if (_CopyCommand[0] == null)
                {
                    _CopyCommand[0] = new RelayCommand((p) => {
                        OnCopy(1);
                    });
                }
                return _CopyCommand[0];
            }
        }
        public ICommand Copy2Command
        {
            get
            {
                if (_CopyCommand[1] == null)
                {
                    _CopyCommand[1] = new RelayCommand((p) => {
                        OnCopy(2);
                    });
                }
                return _CopyCommand[1];
            }
        }
        public ICommand Copy3Command
        {
            get
            {
                if (_CopyCommand[2] == null)
                {
                    _CopyCommand[2] = new RelayCommand((p) => {
                        OnCopy(3);
                    });
                }
                return _CopyCommand[2];
            }
        }
        public ICommand Copy4Command
        {
            get
            {
                if (_CopyCommand[3] == null)
                {
                    _CopyCommand[3] = new RelayCommand((p) => {
                        OnCopy(4);
                    });
                }
                return _CopyCommand[3];
            }
        }
        public ICommand Copy5Command
        {
            get
            {
                if (_CopyCommand[4] == null)
                {
                    _CopyCommand[4] = new RelayCommand((p) => {
                        OnCopy(5);
                    });
                }
                return _CopyCommand[4];
            }
        }
        private void OnCopy(int index)
        {
            if (Copy != null)
            {
                Copy(this, new CutCopyEventArgs(_Turnos[index - 1]));
            }
        }

        public ICommand Cut1Command
        {
            get
            {
                if (_CutCommand[0] == null)
                {
                    _CutCommand[0] = new RelayCommand((p) => {
                        OnCut(1);
                    });
                }
                return _CutCommand[0];
            }
        }
        public ICommand Cut2Command
        {
            get
            {
                if (_CutCommand[1] == null)
                {
                    _CutCommand[1] = new RelayCommand((p) => {
                        OnCut(2);
                    });
                }
                return _CutCommand[1];
            }
        }
        public ICommand Cut3Command
        {
            get
            {
                if (_CutCommand[2] == null)
                {
                    _CutCommand[2] = new RelayCommand((p) => {
                        OnCut(3);
                    });
                }
                return _CutCommand[2];
            }
        }
        public ICommand Cut4Command
        {
            get
            {
                if (_CutCommand[3] == null)
                {
                    _CutCommand[3] = new RelayCommand((p) => {
                        OnCut(4);
                    });
                }
                return _CutCommand[3];
            }
        }
        public ICommand Cut5Command
        {
            get
            {
                if (_CutCommand[4] == null)
                {
                    _CutCommand[4] = new RelayCommand((p) => {
                        OnCut(5);
                    });
                }
                return _CutCommand[4];
            }
        }
        private void OnCut(int index)
        {
            if (Cut != null)
            {
                Cut(this, new CutCopyEventArgs(_Turnos[index - 1]));
            }
        }

        public ICommand Delete1Command
        {
            get
            {
                if (_DeleteCommand[0] == null)
                {
                    _DeleteCommand[0] = new RelayCommand((p) => {
                        OnDelete(1);
                    });
                }
                return _DeleteCommand[0];
            }
        }
        public ICommand Delete2Command
        {
            get
            {
                if (_DeleteCommand[1] == null)
                {
                    _DeleteCommand[1] = new RelayCommand((p) => {
                        OnDelete(2);
                    });
                }
                return _DeleteCommand[1];
            }
        }
        public ICommand Delete3Command
        {
            get
            {
                if (_DeleteCommand[2] == null)
                {
                    _DeleteCommand[2] = new RelayCommand((p) => {
                        OnDelete(3);
                    });
                }
                return _DeleteCommand[2];
            }
        }
        public ICommand Delete4Command
        {
            get
            {
                if (_DeleteCommand[3] == null)
                {
                    _DeleteCommand[3] = new RelayCommand((p) => {
                        OnDelete(4);
                    });
                }
                return _DeleteCommand[3];
            }
        }
        public ICommand Delete5Command
        {
            get
            {
                if (_DeleteCommand[4] == null)
                {
                    _DeleteCommand[4] = new RelayCommand((p) => {
                        OnDelete(5);
                    });
                }
                return _DeleteCommand[4];
            }
        }
        private void OnDelete(int index)
        {
            if (Delete != null)
            {
                Delete(this, new CutCopyEventArgs(_Turnos[index - 1]));
                _Turnos[index - 1] = null;
                RaiseAllProperties();
            }
        }

        public ICommand Click1Command
        {
            get
            {
                if (_ClickCommand[0] == null)
                {
                    _ClickCommand[0] = new RelayCommand((p) => {
                        OnClick(1);
                    });
                }
                return _ClickCommand[0];
            }
        }
        public ICommand Click2Command
        {
            get
            {
                if (_ClickCommand[1] == null)
                {
                    _ClickCommand[1] = new RelayCommand((p) => {
                        OnClick(2);
                    });
                }
                return _ClickCommand[1];
            }
        }
        public ICommand Click3Command
        {
            get
            {
                if (_ClickCommand[2] == null)
                {
                    _ClickCommand[2] = new RelayCommand((p) => {
                        OnClick(3);
                    });
                }
                return _ClickCommand[2];
            }
        }
        public ICommand Click4Command
        {
            get
            {
                if (_ClickCommand[3] == null)
                {
                    _ClickCommand[3] = new RelayCommand((p) => {
                        OnClick(4);
                    });
                }
                return _ClickCommand[3];
            }
        }
        public ICommand Click5Command
        {
            get
            {
                if (_ClickCommand[4] == null)
                {
                    _ClickCommand[4] = new RelayCommand((p) => {
                        OnClick(5);
                    });
                }
                return _ClickCommand[4];
            }
        }
        private void OnClick(int index)
        {
            if (_Turnos[index - 1] == null)
            {
                _Turnos[index - 1] = _OrdenCallback(_Dia, _TurnoHora, index);
            }
            else
            {
                // TODO: Marcar presente
            }
            RaiseAllProperties();
        }

        private void RaiseAllProperties()
        {
            RaisePropertyChanged("Text1");
            RaisePropertyChanged("Text2");
            RaisePropertyChanged("Text3");
            RaisePropertyChanged("Text4");
            RaisePropertyChanged("Text5");
            RaisePropertyChanged("Delete1Visibility");
            RaisePropertyChanged("Delete2Visibility");
            RaisePropertyChanged("Delete3Visibility");
            RaisePropertyChanged("Delete4Visibility");
            RaisePropertyChanged("Delete5Visibility");
            RaisePropertyChanged("Paste1Visibility");
            RaisePropertyChanged("Paste2Visibility");
            RaisePropertyChanged("Paste3Visibility");
            RaisePropertyChanged("Paste4Visibility");
            RaisePropertyChanged("Paste5Visibility");
            RaisePropertyChanged("Orden1");
            RaisePropertyChanged("Orden2");
            RaisePropertyChanged("Orden3");
            RaisePropertyChanged("Orden4");
            RaisePropertyChanged("Orden5");
        }

        public Visibility Paste1Visibility
        {
            get
            {
                return _Turnos[0] == null ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility Paste2Visibility
        {
            get
            {
                return _Turnos[1] == null ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility Paste3Visibility
        {
            get
            {
                return _Turnos[2] == null ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility Paste4Visibility
        {
            get
            {
                return _Turnos[3] == null ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility Paste5Visibility
        {
            get
            {
                return _Turnos[4] == null ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility Delete1Visibility
        {
            get
            {
                return _Turnos[0] == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public Visibility Delete2Visibility
        {
            get
            {
                return _Turnos[1] == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public Visibility Delete3Visibility
        {
            get
            {
                return _Turnos[2] == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public Visibility Delete4Visibility
        {
            get
            {
                return _Turnos[3] == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public Visibility Delete5Visibility
        {
            get
            {
                return _Turnos[4] == null ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public override string Title
        {
            get
            {
                return "Slot";
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
    }
}
