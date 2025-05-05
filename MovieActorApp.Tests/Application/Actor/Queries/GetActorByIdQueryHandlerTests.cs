using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.Actors.Queries;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;

public class GetActorByIdQueryHandlerTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly GetActorByIdQueryHandler _handler;

    public GetActorByIdQueryHandlerTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _handler = new GetActorByIdQueryHandler(_actorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ActorExists_ReturnsActorResponse()
    {
        // Arrange
        var actor = new Actor { Id = 1, FirstName = "Robert", LastName = "Downey" };

        _actorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(actor);

        var query = new GetActorByIdQuery(1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.FirstName.Should().Be("Robert");
        result.LastName.Should().Be("Downey");

        _actorRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_ActorDoesNotExist_ReturnsNull()
    {
        // Arrange
        _actorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(99))
            .ReturnsAsync((Actor?)null);

        var query = new GetActorByIdQuery(99);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _actorRepositoryMock.Verify(repo => repo.GetByIdAsync(99), Times.Once);
    }
}
