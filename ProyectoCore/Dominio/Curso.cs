using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public int        CursoID          { get; set; }
        public string?    Titulo           { get; set; }
        public string?    Descripcion      { get; set; }
        public DateTime   FechaPublicacion { get; set; }
        public byte[]?    FotoPortada      { get; set; }

       
        public ICollection <CursoInstructor>? CursoInstructor { get; set; }
        public ICollection <Comentario>?      Comentario      { get; set; }
        public ICollection <Precio>?          Precio          { get; set; }


        //public CursoInstructor? CursoInstructor { get; set; }
        //public Precio?          Precio          { get; set; }
        //public Comentario?      Comentario      { get; set; }


    }



}
