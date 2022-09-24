using Aplicacion.ManejadorErrores;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class DeleteCurso
    {
        public class EliminarCurso: IRequest
        {
            public int Id { get; set; }
        }
    
        public class Manejador : IRequestHandler<EliminarCurso>
        {
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(EliminarCurso request, CancellationToken cancellationToken)
            {

                var curso = await _context.Curso.FindAsync(request.Id);


                if (curso is null)
                {
                    throw new ExcepcionError(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }
                _context.Remove(curso);
                    
              
                var resultado = await _context.SaveChangesAsync();
                throw new Exception("Se elimino Curso");
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new ExcepcionError(HttpStatusCode.NotFound, new { mensaje = "No se guardaron los cambios" });

            }

                

        }
    }
}
