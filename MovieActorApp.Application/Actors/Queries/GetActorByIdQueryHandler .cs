using MediatR;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Queries
{
    public class GetActorByIdQueryHandler : IRequestHandler<GetActorByIdQuery, ActorResponse?>
    {
        private readonly IActorRepository _actorRepository;

        public GetActorByIdQueryHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ActorResponse?> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
        {
            var actor = await _actorRepository.GetByIdAsync(request.Id);
            return actor is null ? null : ActorResponse.FromEntity(actor);
        }
    }
}

