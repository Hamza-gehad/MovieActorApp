

namespace MovieActorApp.Tests.Application.Movie.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Moq;
    using FluentAssertions;
    using MovieActorApp.Application.Movies.Queries;
    using MovieActorApp.Application.DTO;
    using MovieActorApp.Application.Interfaces;
    using MovieActorApp.Domain.Models;

    public class GetAllMoviesQueryHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly GetAllMoviesQueryHandler _handler;

        public GetAllMoviesQueryHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new GetAllMoviesQueryHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenMoviesExist_ReturnsListOfMovieResponses()
        {
            // Arrange
            var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Rating = 9.0, ReleaseDate = new DateOnly(2010, 7, 16), Director = "Christopher Nolan", Description = "Dreams" },
            new Movie { Id = 2, Title = "The Matrix", Genre = "Action", Rating = 8.7, ReleaseDate = new DateOnly(1999, 3, 31), Director = "The Wachowskis", Description = "Simulation" }
        };

            _movieRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(movies);

            var query = new GetAllMoviesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Title.Should().Be("Inception");

            _movieRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenNoMoviesExist_ReturnsEmptyList()
        {
            // Arrange
            _movieRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Movie>());

            var query = new GetAllMoviesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _movieRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }

}
