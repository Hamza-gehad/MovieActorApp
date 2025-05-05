using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.Actors.Commands;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;

public class UpdateActorCommandHandlerTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly UpdateActorCommandHandler _handler;

    public UpdateActorCommandHandlerTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _handler = new UpdateActorCommandHandler(_actorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidUpdate_ReturnsActorResponse()
    {
        // Arrange
        var dto = new ActorRequest { FirstName = "Ryan", LastName = "Gosling" };
        var command = new UpdateActorCommand(1, dto);

        var actorEntity = new Actor { Id = 1, FirstName = dto.FirstName, LastName = dto.LastName };

        _actorRepositoryMock
            .Setup(repo => repo.UpdateActorAsync(command.Id, command.Dto))
            .ReturnsAsync(actorEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be(dto.FirstName);
        result.LastName.Should().Be(dto.LastName);
        _actorRepositoryMock.Verify(repo => repo.UpdateActorAsync(command.Id, command.Dto), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateFails_ReturnsNull()
    {
        // Arrange
        var dto = new ActorRequest { FirstName = "Invalid", LastName = "Name" };
        var command = new UpdateActorCommand(999, dto);

        _actorRepositoryMock
            .Setup(repo => repo.UpdateActorAsync(command.Id, command.Dto))
            .ReturnsAsync((Actor?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _actorRepositoryMock.Verify(repo => repo.UpdateActorAsync(command.Id, command.Dto), Times.Once);
    }
}

