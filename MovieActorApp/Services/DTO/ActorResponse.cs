using MovieActorApp.Models;

namespace MovieActorApp.Services.DTO
{
    public class ActorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Biography { get; set; }
        public List<MovieSummary> Movies { get; set; } = new();

        // 🔹 Converts an Actor entity to ActorResponseDto
        public static ActorResponse FromEntity(Actor actor)
        {
            return new ActorResponse
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                DateOfBirth = actor.DateOfBirth,
                Biography = actor.Biography,
                Movies = actor.Movies.Select(MovieSummary.FromEntity).ToList()
            };
        }
    }
}
