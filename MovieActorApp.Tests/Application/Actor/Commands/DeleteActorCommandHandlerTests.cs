using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.Actors.Commands;
using MovieActorApp.Application.Interfaces;

public class DeleteActorCommandHandlerTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly DeleteActorCommandHandler _handler;

    public DeleteActorCommandHandlerTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _handler = new DeleteActorCommandHandler(_actorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsTrue()
    {
        // Arrange
        var command = new DeleteActorCommand(1);

        _actorRepositoryMock
            .Setup(repo => repo.DeleteActorAsync(command.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _actorRepositoryMock.Verify(repo => repo.DeleteActorAsync(command.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsFalse()
    {
        // Arrange
        var command = new DeleteActorCommand(999);

        _actorRepositoryMock
            .Setup(repo => repo.DeleteActorAsync(command.Id))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _actorRepositoryMock.Verify(repo => repo.DeleteActorAsync(command.Id), Times.Once);
    }
}
