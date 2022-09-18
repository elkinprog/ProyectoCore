using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class PostCurso
    {

        public class CrearCurso: IRequest
        {
            public string?  Titulo           { get; set; }
            public string?  Descripcion      { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public byte[]?  FotoPortada      { get; set; }
         }

        public class Manejador : IRequestHandler<CrearCurso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                this._context = context;
            }
             async Task<Unit> IRequestHandler<CrearCurso, Unit>.Handle(CrearCurso request, CancellationToken cancellationToken)
             {
                var cursos = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FotoPortada = request.FotoPortada,
                };
                _context.Curso.AddAsync(cursos);
               var valor = await _context.SaveChangesAsync();
                if (valor > 0) 
                { 
                 return Unit.Value;    
                }
                throw new Exception("No se pudo Insertar Data");
             }
        }

    }
}
