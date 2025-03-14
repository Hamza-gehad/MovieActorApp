using Microsoft.EntityFrameworkCore;
using MovieActorApp.Models;

namespace MovieActorApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>().ToTable("Actors");
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Actor>()
                .HasMany(a => a.Movies)
                .WithMany(m => m.Actors)
                .UsingEntity(j => j.ToTable("ActorMovies"));
            modelBuilder.Entity<Actor>().HasData(
     new Actor
     {
         Id = 1,
         FirstName = "Leonardo",
         LastName = "DiCaprio",
         DateOfBirth = new DateTime(1974, 11, 11),
         Biography = "Leonardo Wilhelm DiCaprio is an American actor and film producer."
     },
     new Actor
     {
         Id = 2,
         FirstName = "Tom",
         LastName = "Hanks",
         DateOfBirth = new DateTime(1956, 7, 9),
         Biography = "Thomas Jeffrey Hanks is an American actor and filmmaker."
     },
     new Actor
     {
         Id = 3,
         FirstName = "Meryl",
         LastName = "Streep",
         DateOfBirth = new DateTime(1949, 6, 22),
         Biography = "Meryl Streep is an American actress often described as the best actress of her generation."
     }
 );

            // Seed data for Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Genre = "Sci-Fi",
                    Rating = 8.8,
                    Director = "Christopher Nolan"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Forrest Gump",
                    Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75.",
                    ReleaseDate = new DateTime(1994, 7, 6),
                    Genre = "Drama",
                    Rating = 8.8,
                    Director = "Robert Zemeckis"
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Devil Wears Prada",
                    Description = "A smart but sensible new graduate lands a job as an assistant to Miranda Priestly, the demanding editor-in-chief of a high-fashion magazine.",
                    ReleaseDate = new DateTime(2006, 6, 30),
                    Genre = "Comedy",
                    Rating = 6.9,
                    Director = "David Frankel"
                }
            );

            
            modelBuilder.Entity<Actor>().HasMany(a => a.Movies).WithMany(m => m.Actors)
                .UsingEntity<Dictionary<string, object>>(
                    "ActorMovies",
                    j => j.HasData(
                        new { ActorsId = 1, MoviesId = 1 }, 
                        new { ActorsId = 2, MoviesId = 2 }, 
                        new { ActorsId = 3, MoviesId = 3 }  
                    )
                );
        }
    }


}
    
