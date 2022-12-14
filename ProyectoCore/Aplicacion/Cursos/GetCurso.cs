using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Aplicacion.ManejadorErrores;
using static Aplicacion.Cursos.GetIdCurso;
using System.Net;

namespace Aplicacion.Cursos
{
    public  class GetCurso
    {
        public class ListaCursos: IRequest<List<Curso>>{}

        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async  Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Curso.ToListAsync();
                return cursos;

            }
         }
    }
}
