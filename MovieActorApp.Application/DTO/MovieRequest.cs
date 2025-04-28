namespace MovieActorApp.Application.DTO
{
    public class MovieRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
        public string? Director { get; set; }

        // List of actors (first & last name)
        public List<ActorSummary> Actors { get; set; } = new();

    }
}
