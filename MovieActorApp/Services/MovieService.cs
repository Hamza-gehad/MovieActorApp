using MovieActorApp.Repositories;
using MovieActorApp.Services.DTO;

namespace MovieActorApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse> CreateMovieAsync(MovieRequest dto)
        {
            var movie = await _movieRepository.AddMovieAsync(dto);
            return MovieResponse.FromEntity(movie);
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(MovieResponse.FromEntity);
        }

        public async Task<MovieResponse?> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie is null ? null : MovieResponse.FromEntity(movie);
        }
        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _movieRepository.DeleteMovieAsync(id);
        }

        public async Task<MovieResponse?> UpdateMovieAsync(int id, MovieRequest dto)
        {
            var updatedMovie = await _movieRepository.UpdateMovieAsync(id, dto);
            return updatedMovie is null ? null : MovieResponse.FromEntity(updatedMovie);
        }

    }

}
