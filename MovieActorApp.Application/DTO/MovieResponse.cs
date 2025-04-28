using MovieActorApp.Domain.Models;

namespace MovieActorApp.Application.DTO
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public double? Rating { get; set; }
        public string? Director { get; set; }
        public List<ActorSummary> Actors { get; set; } = new();

        
        public static MovieResponse FromEntity(Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Rating = movie.Rating,
                Director = movie.Director,
                Actors = movie.Actors.Select(ActorSummary.FromEntity).ToList()
            };
        }
    }
}
