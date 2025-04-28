using MediatR;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Commands
{
    public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, ActorResponse>
    {
        private readonly IActorRepository _actorRepository;

        public CreateActorCommandHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ActorResponse> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _actorRepository.AddActorAsync(request.Dto);
            return ActorResponse.FromEntity(actor);
        }
    }
}
