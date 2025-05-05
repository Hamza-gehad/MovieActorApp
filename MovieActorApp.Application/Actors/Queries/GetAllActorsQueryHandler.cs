using MediatR;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Queries
{
    public class GetAllActorsQueryHandler : IRequestHandler<GetAllActorsQuery, IEnumerable<ActorResponse>>
    {
        private readonly IActorRepository _actorRepository;

        public GetAllActorsQueryHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<ActorResponse>> Handle(GetAllActorsQuery request, CancellationToken cancellationToken)
        {
            var actors = await _actorRepository.GetAllAsync();
            return actors.Select(ActorResponse.FromEntity);
        }
    }
}

