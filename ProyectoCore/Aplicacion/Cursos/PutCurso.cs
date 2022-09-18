using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public  class PutCurso
    {

        public class EditarCurso: IRequest
        {
            public int       CursoID           { get; set; }
            public string?   Titulo            { get; set; }
            public string?   Descripcion       { get; set; }
            public DateTime? FechaPublicacion  { get; set; }
            public byte[]?   FotoPortada       { get; set; }
        }

        public class Manejador : IRequestHandler<EditarCurso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                this._context = context;
            }

            async Task<Unit> IRequestHandler<EditarCurso, Unit>.Handle(EditarCurso request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoID);
                if(curso == null)
                {
                    throw  new Exception("El Curso no existe");
                }

                curso.Titulo            = request.Titulo ??          curso.Titulo;
                curso.Descripcion       = request.Descripcion ??     curso.Descripcion;
                curso.FechaPublicacion  = request.FechaPublicacion?? curso.FechaPublicacion;
                curso.FotoPortada       = request.FotoPortada ??     curso.FotoPortada;

                var resultado = await _context.SaveChangesAsync();
                if (resultado>0)
                {
                    return Unit.Value;
                }
                throw new Exception("NO se guardaronlos cambios");
            }
        }
    }
}
