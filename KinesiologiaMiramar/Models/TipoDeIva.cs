using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinesiologiaMiramar.Models
{
    public class TipoDeIva
    {
        public string Code { get; set; }
        public string Descripcion { get; set; }

        public TipoDeIva(string Code, string Descripcion)
        {
            this.Code = Code;
            this.Descripcion = Descripcion;
        }

        public static TipoDeIva[] Tipos = new TipoDeIva[] {
            new TipoDeIva("E", "Exento"),
            new TipoDeIva("G", "Gravado"),
            new TipoDeIva("O", "Obligatorio"),
            new TipoDeIva("V", "Voluntario individual"),
            new TipoDeIva("C", "Colectivo"),
            new TipoDeIva("D", "Discapacidad")
        };

        public static string DescripcionDe(string code)
        {
            return Tipos.Where(t => t.Code == code).First().Descripcion;
        }

        public static string[] Descripcions
        {
            get
            {
                return Tipos.Select(t => t.Descripcion).ToArray();
            }
        }

        public static string[] Codigos
        {
            get
            {
                return Tipos.Select(t => t.Code).ToArray();
            }
        }
    }
}
