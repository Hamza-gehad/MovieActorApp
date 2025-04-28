

namespace MovieActorApp.Application.DTO
{
    public class ActorRequest 
    {
        public string FirstName { get; set; }
        public string LastName { get;  set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Biography { get; set; }


        // List of movies (title + release date)
        public List<MovieSummary> Movies { get; set; } = new();

    }

 
}
