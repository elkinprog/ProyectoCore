using Aplicacion.ManejadorErrores;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Aplicacion.Cursos.GetIdCurso;

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

        public class PostValidacion : AbstractValidator<CrearCurso>
        {
            public PostValidacion()
            {
                RuleFor(x => x.Titulo).NotNull();
                RuleFor(x => x.Descripcion).NotNull();
                RuleFor(x => x.FechaPublicacion).NotNull();
            }
        }



        public class Manejador : IRequestHandler<CrearCurso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
               _context = context;
            }
             async Task<Unit> IRequestHandler<CrearCurso, Unit>.Handle(CrearCurso request, CancellationToken cancellationToken)
             {
                var  cursos = new Curso
                {
                    Titulo           = request.Titulo,
                    Descripcion      = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FotoPortada      = request.FotoPortada,
                };

                await _context.Curso.AddAsync(cursos, cancellationToken);



               var valor = await _context.SaveChangesAsync(cancellationToken);
                throw new ExcepcionError(HttpStatusCode.OK, new { mensaje = "Se Ingreso curso" });
                if (valor > 0) 
                { 
                 return Unit.Value;    
                }
                throw new Exception("No se pudo Insertar Data");
             }
        }

    }
}
