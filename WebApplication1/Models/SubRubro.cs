using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SubRubro
    {
        [Key]
        public int SubRubroID { get; set; }
        public string Descripcion { get; set; }

        public int RubroID { get; set; }

        public bool Eliminado { get; set; }

        public virtual Rubro Rubro { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }


    }

    public class SubRubroMostrar
    {
        public int SubRubroID { get; set; }
        public string Descripcion { get; set; }

        public int RubroID { get; set; }

        public string RubroNombre { get; set; }

        public bool Eliminado { get; set; }

    }

}
