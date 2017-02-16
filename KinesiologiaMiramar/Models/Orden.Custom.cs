using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.Models
{
    public partial class Orden
    {
        public int SesionesRestantes
        {
            get
            {
                return Sesiones - SesionesUsadas; 
            }
        }
    }
}
