using MovieActorApp.Application.DTO;

namespace MovieActorApp.Application.Interfaces
{
    public interface IMovieService
    {
        Task<MovieResponse> CreateMovieAsync(MovieRequest dto);
        Task<IEnumerable<MovieResponse>> GetAllMoviesAsync();
        Task<MovieResponse?> GetMovieByIdAsync(int id);
        Task<bool> DeleteMovieAsync(int id);
        Task<MovieResponse?> UpdateMovieAsync(int id, MovieRequest dto);

    }
}
