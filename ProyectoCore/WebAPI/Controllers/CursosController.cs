using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
        {

        private readonly IMediator _mediator;
        public CursosController(IMediator mediator)
            {
            this._mediator = mediator;
            }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new GetCurso.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetId(int id)
        {
            return await _mediator.Send(new GetIdCurso.CursoId { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(PostCurso.CrearCurso data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Unit>> Put(int id, PutCurso.EditarCurso data)
        {
            data.CursoID = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteCurso.EliminarCurso { Id = id });
        }

    }
}
