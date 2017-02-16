using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.Models
{
    public partial class Paciente
    {
        public string ApellidoNombre
        {
            get
            {
                return string.Format("{0}, {1}", Apellido, Nombre);
            }
        }
        public string NombreApellido
        {
            get
            {
                return string.Format("{0} {1}", Nombre, Apellido);
            }
        }

        public string EdadHoy
        {
            get
            {
                return Edad(DateTime.Today);
            }
        }

        public string Edad(DateTime basis)
        {
            if (FecNac.HasValue)
            {
                DateTime fecnac = FecNac.Value;
                if (fecnac.Day == 29 && fecnac.Month == 2)
                {
                    fecnac = fecnac.AddDays(1);
                }
                if (100 * fecnac.Month + fecnac.Day < 100 * basis.Month + basis.Day)
                {
                    return string.Format("{0}", basis.Year - FecNac.Value.Year);
                }
                else
                {
                    return string.Format("{0}", basis.Year - FecNac.Value.Year - 1);
                }
            }
            else
            {
                return string.Empty;
            }            
        }
    }
}
