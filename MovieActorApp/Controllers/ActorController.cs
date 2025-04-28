using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Actors.Commands;
using MovieActorApp.Application.Actors.Queries;

namespace MovieActorApp.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/actors
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ActorResponse>> CreateActor([FromBody] ActorRequest dto)
        {
            var command = new CreateActorCommand(dto);
            var createdActor = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetActorById), new { id = createdActor.Id }, createdActor);
        }

        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorResponse>>> GetAllActors()
        {
            var query = new GetAllActorsQuery();
            var actors = await _mediator.Send(query);
            return Ok(actors);
        }

        // GET: api/actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorResponse>> GetActorById(int id)
        {
            var query = new GetActorByIdQuery(id);
            var actor = await _mediator.Send(query);
            return actor is null ? NotFound() : Ok(actor);
        }

        // PUT: api/actors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorRequest dto)
        {
            var command = new UpdateActorCommand(id, dto);
            var updatedActor = await _mediator.Send(command);
            return updatedActor is null ? NotFound() : Ok(updatedActor);
        }

        // DELETE: api/actors/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var command = new DeleteActorCommand(id);
            var success = await _mediator.Send(command);
            return success ? Ok("Actor deleted successfully") : NotFound("Actor not found");
        }
    }
}
