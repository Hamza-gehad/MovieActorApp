using Microsoft.EntityFrameworkCore;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;
using MovieActorApp.Infrastructure.Data;
using MovieActorApp.Infrastructure.Middlewares;



namespace MovieActorApp.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _context.Actors.Include(a => a.Movies).ToListAsync();
        }

        public async Task<Actor> AddActorAsync(ActorRequest dto)
        {
            try
            {
                var existingMovies = await _context.Movies
                    .Where(m => dto.Movies.Select(md => md.Title).Contains(m.Title) &&
                                dto.Movies.Select(md => md.ReleaseDate).Contains(m.ReleaseDate))
                    .ToListAsync();

                var newMovies = dto.Movies
                    .Where(md => !existingMovies.Any(em => em.Title == md.Title && em.ReleaseDate == md.ReleaseDate))
                    .Select(md => new Movie { Title = md.Title, ReleaseDate = md.ReleaseDate })
                    .ToList();

                if (newMovies.Any())
                {
                    _context.Movies.AddRange(newMovies);
                    await _context.SaveChangesAsync();
                }

                var allMovies = existingMovies.Concat(newMovies).ToList();

                var actor = new Actor
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Biography = dto.Biography,
                    Movies = allMovies
                };

                _context.Actors.Add(actor);
                await _context.SaveChangesAsync();
                return actor;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the actor.", ex);
            }
        }

        public async Task<Actor?> GetByIdAsync(int id)
        {
            var actor = await _context.Actors.Include(a => a.Movies).FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
                throw new NotFoundException($"Actor with ID {id} not found.");

            return actor;
        }

        public async Task<bool> DeleteActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                throw new NotFoundException($"Actor with ID {id} not found.");

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Actor?> UpdateActorAsync(int id, ActorRequest dto)
        {
            var actor = await _context.Actors.Include(a => a.Movies).FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
                throw new NotFoundException($"Actor with ID {id} not found.");

            actor.FirstName = dto.FirstName;
            actor.LastName = dto.LastName;
            actor.DateOfBirth = dto.DateOfBirth;
            actor.Biography = dto.Biography;

            actor.Movies.Clear();
            foreach (var movieDto in dto.Movies)
            {
                var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Title == movieDto.Title);
                if (existingMovie == null)
                {
                    existingMovie = new Movie { Title = movieDto.Title, ReleaseDate = movieDto.ReleaseDate };
                    _context.Movies.Add(existingMovie);
                }
                actor.Movies.Add(existingMovie);
            }

            await _context.SaveChangesAsync();
            return actor;
        }
    }
}
