using KinesiologiaMiramar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.ViewModels
{
    public class CutCopyEventArgs : EventArgs
    {
        public Turno Turno { get; set; }

        public CutCopyEventArgs(Turno Turno)
        {
            this.Turno = Turno;
        }
    }
}
