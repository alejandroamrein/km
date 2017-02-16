using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KinesiologiaMiramar.Models;
using KinesiologiaMiramar.Helpers;
using System.Collections.ObjectModel;

namespace KinesiologiaMiramar.ViewModels
{
    public class ObrasSocialesViewModel : ViewModelBase
    {
        private ICommand _AgregarObraSocialCommand;
        private ICommand _ImprimirCommand;
        private ObservableCollection<ObraSocial> _ObrasSociales;
        private bool _IsDirty;

        public ObrasSocialesViewModel()
        {
            var entities = new kmEntities();
            var q = entities.ObrasSociales;
            _ObrasSociales = new ObservableCollection<ObraSocial>(q.ToList());
        }

        public override string Title { get { return "Obras sociales"; } }
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

        public ICommand AgregarObraSocialCommand
        {
            get
            {
                if (_AgregarObraSocialCommand == null)
                {
                    _AgregarObraSocialCommand = new RelayCommand((p) => {
                        MessageBox.Show("Agregar Obra Social Command");
                    });
                }
                return _AgregarObraSocialCommand;
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

        public ObservableCollection<ObraSocial> ObrasSociales
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
    }
}
