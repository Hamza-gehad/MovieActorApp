using Microsoft.EntityFrameworkCore;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;
using MovieActorApp.Infrastructure.Data;
using MovieActorApp.Infrastructure.Middlewares;


namespace MovieActorApp.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovieAsync(MovieRequest dto)
        {
            var existingActors = await _context.Actors
                .Where(a => dto.Actors.Select(ad => ad.FirstName).Contains(a.FirstName) &&
                            dto.Actors.Select(ad => ad.LastName).Contains(a.LastName))
                .ToListAsync();

            var newActors = dto.Actors
                .Where(ad => !existingActors.Any(ea => ea.FirstName == ad.FirstName && ea.LastName == ad.LastName))
                .Select(ad => new Actor { FirstName = ad.FirstName, LastName = ad.LastName })
                .ToList();

            if (newActors.Any())
            {
                _context.Actors.AddRange(newActors);
                await _context.SaveChangesAsync();
            }

            var allActors = existingActors.Concat(newActors).ToList();

            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                Genre = dto.Genre,
                Rating = dto.Rating,
                Director = dto.Director,
                Actors = allActors
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.Include(m => m.Actors).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                throw new NotFoundException($"Movie with ID {id} not found.");

            return movie;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                throw new NotFoundException($"Movie with ID {id} not found.");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Movie> UpdateMovieAsync(int id, MovieRequest dto)
        {
            var movie = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                throw new NotFoundException($"Movie with ID {id} not found.");

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.Genre = dto.Genre;
            movie.Rating = dto.Rating;
            movie.Director = dto.Director;

            movie.Actors.Clear();
            foreach (var actorDto in dto.Actors)
            {
                var existingActor = await _context.Actors.FirstOrDefaultAsync(a => a.FirstName == actorDto.FirstName && a.LastName == actorDto.LastName);
                if (existingActor == null)
                {
                    existingActor = new Actor { FirstName = actorDto.FirstName, LastName = actorDto.LastName };
                    _context.Actors.Add(existingActor);
                }
                movie.Actors.Add(existingActor);
            }

            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
