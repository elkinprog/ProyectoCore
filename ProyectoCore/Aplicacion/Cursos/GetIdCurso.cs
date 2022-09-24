using Aplicacion.ManejadorErrores;
using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
        public  class GetIdCurso
        {
            public class CursoId: IRequest<Curso>
            {
            public int Id { get; set; }
            }

            public class Manejador : IRequestHandler<CursoId, Curso>
            {

                private readonly CursosOnlineContext _context;
                public Manejador(CursosOnlineContext context)
                {
                _context = context;
                }

                public async  Task<Curso> Handle(CursoId request, CancellationToken cancellationToken)
                {
                  var curso = await _context.Curso.FindAsync(request.Id);
          
                if (curso is null)
                {
                 throw new ExcepcionError(HttpStatusCode.NotFound, new { mensaje = "No se encontraron  cursos" });
                }
                return curso;

                }
            }


        }
}
