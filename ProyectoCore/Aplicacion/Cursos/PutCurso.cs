using Aplicacion.ManejadorErrores;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Rewrite;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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


        public class PutValidacion: AbstractValidator<EditarCurso>
        {
            public PutValidacion()
            {
                RuleFor(x => x.CursoID).NotNull();
                RuleFor(x => x.Titulo).NotNull().WithMessage("Titulo Requerido");
                RuleFor(x => x.Descripcion).NotNull().WithMessage("Descripcion Requerida");
                RuleFor(x => x.FechaPublicacion).NotNull().WithMessage("FechaPublicacion Requerida");
            }
        }


        public class Manejador : IRequestHandler<EditarCurso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
               _context = context;
            }

            async Task<Unit> IRequestHandler<EditarCurso, Unit>.Handle(EditarCurso request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(new object?[] { request.CursoID }, cancellationToken: cancellationToken);
                if(curso == null)
                {
                   throw new ExcepcionError(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }

                curso.Titulo            = request.Titulo ??          curso.Titulo;
                curso.Descripcion       = request.Descripcion ??     curso.Descripcion;
                curso.FechaPublicacion  = request.FechaPublicacion?? curso.FechaPublicacion;
                curso.FotoPortada       = request.FotoPortada ??     curso.FotoPortada;

                var resultado = await _context.SaveChangesAsync(cancellationToken);
                throw new ExcepcionError(HttpStatusCode.OK, new { mensaje = "Se actualizo con exito" });
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se guardaronlos cambios");
            }
        }
    }
}
