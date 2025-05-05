using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Application.Movies.Commands;

namespace MovieActorApp.Tests.Application.Movie.Commands
{
    public class DeleteMovieCommandHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly DeleteMovieCommandHandler _handler;

        public DeleteMovieCommandHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new DeleteMovieCommandHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenDeletionSucceeds()
        {
            // Arrange
            var movieId = 1;
            _movieRepositoryMock.Setup(repo => repo.DeleteMovieAsync(movieId))
                                .ReturnsAsync(true);

            var command = new DeleteMovieCommand (movieId );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _movieRepositoryMock.Verify(repo => repo.DeleteMovieAsync(movieId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenDeletionFails()
        {
            // Arrange
            var movieId = 999; // Assume this ID doesn't exist
            _movieRepositoryMock.Setup(repo => repo.DeleteMovieAsync(movieId))
                                .ReturnsAsync(false);

            var command = new DeleteMovieCommand (movieId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _movieRepositoryMock.Verify(repo => repo.DeleteMovieAsync(movieId), Times.Once);
        }
    }
}

