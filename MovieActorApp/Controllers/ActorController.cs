using Microsoft.AspNetCore.Mvc;
using MovieActorApp.Services;
using MovieActorApp.Services.DTO;

namespace MovieActorApp.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] ActorRequest dto)
        {
            var createdActor = await _actorService.CreateActorAsync(dto);
            return CreatedAtAction(nameof(GetActorById), new { id = createdActor.Id }, createdActor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorResponse>>> GetAllActors()
        {
            var actors = await _actorService.GetAllActorsAsync();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorResponse>> GetActorById(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            return Ok(actor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorRequest dto)
        {
            var updatedActor = await _actorService.UpdateActorAsync(id, dto);
            return Ok(updatedActor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            await _actorService.DeleteActorAsync(id);
            return Ok("Actor deleted successfully");
        }
    }
}