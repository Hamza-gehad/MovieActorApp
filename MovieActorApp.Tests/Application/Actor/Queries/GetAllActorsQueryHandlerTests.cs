using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MovieActorApp.Application.Actors.Queries;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using MovieActorApp.Domain.Models;

public class GetAllActorsQueryHandlerTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly GetAllActorsQueryHandler _handler;

    public GetAllActorsQueryHandlerTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _handler = new GetAllActorsQueryHandler(_actorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenCalled_ReturnsAllActors()
    {
        // Arrange
        var actors = new List<Actor>
        {
            new Actor { Id = 1, FirstName = "Leonardo", LastName = "DiCaprio" },
            new Actor { Id = 2, FirstName = "Emma", LastName = "Stone" }
        };

        _actorRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(actors);

        var query = new GetAllActorsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.First().FirstName.Should().Be("Leonardo");
        result.Last().LastName.Should().Be("Stone");

        _actorRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyList_ReturnsEmptyResponse()
    {
        // Arrange
        _actorRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Actor>());

        var query = new GetAllActorsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
        _actorRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}

