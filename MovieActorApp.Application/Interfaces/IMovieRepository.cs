using MovieActorApp.Application.DTO;
using MovieActorApp.Domain.Models;

namespace MovieActorApp.Application.Interfaces
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
