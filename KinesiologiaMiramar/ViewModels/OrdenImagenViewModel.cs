using KinesiologiaMiramar.Helpers;
using KinesiologiaMiramar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.ViewModels
{
    public class OrdenImagenViewModel : ViewModelBase
    {
        private Orden _Orden;
        private double _Scale;

        public OrdenImagenViewModel(Orden orden)
        {
            _Scale = 1;
            _Orden = orden;
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

        public double Scale
        {
            get
            {
                return _Scale;
            }

            set
            {
                _Scale = value;
                RaisePropertyChanged("Scale");
            }
        }

        public override string Title
        {
            get
            {
                return "Imagen";
            }
        }
    }
}
