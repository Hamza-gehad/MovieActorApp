using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Application.Movies.Commands;
using MovieActorApp.Domain.Models;

public class UpdateMovieCommandHandlerTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;
    private readonly UpdateMovieCommandHandler _handler;

    public UpdateMovieCommandHandlerTests()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
        _handler = new UpdateMovieCommandHandler(_movieRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUpdatedMovieResponse()
    {
        // Arrange
        var movieId = 1;
        var dto = new MovieRequest
        {
            Title = "Inception",
            Description = "Mind-bending thriller",
            Genre = "Sci-Fi",
            Director = "Christopher Nolan",
            Rating = 8.8,
            ReleaseDate = new DateOnly(2010, 7, 16),
            Actors = new()
        };

        var updatedMovie = new Movie
        {
            Id = movieId,
            Title = dto.Title,
            Description = dto.Description,
            Genre = dto.Genre,
            Director = dto.Director,
            Rating = dto.Rating,
            ReleaseDate = dto.ReleaseDate,
            Actors = new List<Actor>()
        };

        var command = new UpdateMovieCommand(movieId, dto);

        _movieRepositoryMock
            .Setup(repo => repo.UpdateMovieAsync(movieId, dto))
            .ReturnsAsync(updatedMovie);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be(dto.Title);
        result.Description.Should().Be(dto.Description);

        _movieRepositoryMock.Verify(repo => repo.UpdateMovieAsync(movieId, dto), Times.Once);
    }

    [Fact]
    public async Task Handle_MovieNotFound_ReturnsNull()
    {
        // Arrange
        var movieId = 42;
        var dto = new MovieRequest
        {
            Title = "Nonexistent",
            Description = "No such movie",
            Genre = "Drama",
            Director = "Unknown",
            Rating = 5.0,
            ReleaseDate = new DateOnly(2000, 1, 1),
            Actors = new()
        };

        var command = new UpdateMovieCommand(movieId, dto);

        _movieRepositoryMock
            .Setup(repo => repo.UpdateMovieAsync(movieId, dto))
            .ReturnsAsync((Movie?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _movieRepositoryMock.Verify(repo => repo.UpdateMovieAsync(movieId, dto), Times.Once);
    }
}
