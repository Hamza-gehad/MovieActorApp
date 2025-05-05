using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.Actors.Commands;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;

public class CreateActorCommandHandlerTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly CreateActorCommandHandler _handler;

    public CreateActorCommandHandlerTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _handler = new CreateActorCommandHandler(_actorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsActorResponse()
    {
        // Arrange
        var dto = new ActorRequest
        {
            FirstName = "Christian",
            LastName = "Bale"
        };

        var actorEntity = new Actor
        {
            Id = 10,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        _actorRepositoryMock
            .Setup(repo => repo.AddActorAsync(dto))
            .ReturnsAsync(actorEntity);

        var command = new CreateActorCommand(dto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(dto.FirstName);
        result.LastName.Should().Be(dto.LastName);
        result.Id.Should().Be(actorEntity.Id);

        _actorRepositoryMock.Verify(repo => repo.AddActorAsync(dto), Times.Once);
    }

    [Fact]
    public async Task Handle_NullDto_ThrowsException()
    {
        // Arrange
        var command = new CreateActorCommand(null!);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}

