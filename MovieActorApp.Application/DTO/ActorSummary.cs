using MovieActorApp.Domain.Models;

namespace MovieActorApp.Application.DTO
{
    public class ActorSummary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Converts Actor entity to ActorDto
        public static ActorSummary FromEntity(Actor actor)
        {
            return new ActorSummary
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName
            };
        }
    }
}
