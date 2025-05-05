

namespace MovieActorApp.Tests.Application.Movie.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Moq;
    using FluentAssertions;
    using MovieActorApp.Application.Movies.Queries;
    using MovieActorApp.Application.Interfaces;
    using MovieActorApp.Domain.Models;

    public class GetMovieByIdQueryHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly GetMovieByIdQueryHandler _handler;

        public GetMovieByIdQueryHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new GetMovieByIdQueryHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ExistingMovieId_ReturnsMovieResponse()
        {
            // Arrange
            var movieId = 1;
            var movie = new Movie
            {
                Id = movieId,
                Title = "The Prestige",
                Description = "Magicians",
                Genre = "Drama",
                Director = "Christopher Nolan",
                Rating = 8.5,
                ReleaseDate = new DateOnly(2006, 10, 20),
                Actors = new List<Actor>()
            };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(movie);

            var query = new GetMovieByIdQuery(movieId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("The Prestige");
            result.Id.Should().Be(movieId);

            _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movieId), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingMovieId_ReturnsNull()
        {
            // Arrange
            var movieId = 999;

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync((Movie?)null);

            var query = new GetMovieByIdQuery(movieId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();

            _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movieId), Times.Once);
        }
    }

}
