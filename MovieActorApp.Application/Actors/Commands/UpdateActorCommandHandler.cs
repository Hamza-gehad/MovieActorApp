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
    public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand, ActorResponse?>
    {
        private readonly IActorRepository _actorRepository;

        public UpdateActorCommandHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ActorResponse?> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            var updatedActor = await _actorRepository.UpdateActorAsync(request.Id, request.Dto);
            return updatedActor is null ? null : ActorResponse.FromEntity(updatedActor);
        }
    }
}
