using MovieActorApp.Models;
using MovieActorApp.Services.DTO;

namespace MovieActorApp.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(MovieRequest dto);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<bool> DeleteMovieAsync(int id);
        Task<Movie?> UpdateMovieAsync(int id, MovieRequest dto);

    }
}
