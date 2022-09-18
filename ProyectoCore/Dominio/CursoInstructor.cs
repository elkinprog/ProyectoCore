using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CursoInstructor
    {
        public int         InstructorID { get; set; }
        public int         CursoID      { get; set; }
        public Curso?      Curso        { get; set; }
        public Instructor? Instructor   { get; set; }
    }
}
