  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        public int      ComentarioID    { get; set; }
        public string?  Alumno          { get; set; }
        [Column(TypeName= "decimal(18,4)")]
        public decimal  Puntaje         { get; set; }
        public string?  ComentarioTexto { get; set; }
        public int      CursoID         { get; set; }

        public Curso?   Curso           { get; set; }

    }
}
