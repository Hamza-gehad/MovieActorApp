using MovieActorApp.Models;

namespace MovieActorApp.Services.DTO
{
    public class MovieSummary
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        // Converts Movie entity to MovieDto
        public static MovieSummary FromEntity(Movie movie)
        {
            return new MovieSummary
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate
            };
        }
    }
    }

