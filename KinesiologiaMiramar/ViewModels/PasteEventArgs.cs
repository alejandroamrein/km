using KinesiologiaMiramar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.ViewModels
{
    public class PasteEventArgs : EventArgs
    {
        public DateTime Dia { get; set; }
        public int TurnoHora { get; set; }
        public int Slot { get; set; }

        public PasteEventArgs(DateTime Dia, int TurnoHora, int Slot)
        {
            this.Dia = Dia;
            this.TurnoHora = TurnoHora;
            this.Slot = Slot;
        }
    }
}
