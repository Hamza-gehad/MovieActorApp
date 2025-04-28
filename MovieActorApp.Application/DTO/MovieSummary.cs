using MovieActorApp.Domain.Models;

namespace MovieActorApp.Application.DTO
{
    public class MovieSummary
    {
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }

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

