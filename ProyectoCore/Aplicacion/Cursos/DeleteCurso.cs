using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if(curso == null)
                {
                    throw new Exception("No se puede eliminar curdo");
                }
                _context.Remove(curso);

                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se guardaron los cambios");
                }
        }
    }
}
