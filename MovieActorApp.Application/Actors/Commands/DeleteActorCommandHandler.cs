using MediatR;
using MovieActorApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Commands
{

    public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand, bool>
    {
        private readonly IActorRepository _actorRepository;

        public DeleteActorCommandHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<bool> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            return await _actorRepository.DeleteActorAsync(request.Id);
        }
    }
}
