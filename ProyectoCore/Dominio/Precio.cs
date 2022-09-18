 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
        public int      PrecioID      { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal  PrecioActual  { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal  Promocion     { get; set; }
        public int      CursoID       { get; set; }

        public Curso   Curso         { get; set; }
    }
}
